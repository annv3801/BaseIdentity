using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.Booking.Commons;
using Application.DMP.Booking.Services;
using Application.DMP.Category.Repositories;
using Application.DMP.Seat.Repositories;
using Application.DTO.DMP.Booking.Requests;
using Application.DTO.VnPay;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.DMP.Booking.Services;
public class BookingManagementService : IBookingManagementService
{
    private readonly ILoggerService _loggerService;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IEmailService _emailService;
    private readonly ISeatRepository _seatRepository;
    private readonly IVnPayService _vnPayService;

    public BookingManagementService(ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService, IApplicationDbContext applicationDbContext, ICategoryRepository categoryRepository, IMediator mediator, IEmailService emailService, ISeatRepository seatRepository, IVnPayService vnPayService)
    {
        _loggerService = loggerService;
        _applicationDbContext = applicationDbContext;
        _emailService = emailService;
        _seatRepository = seatRepository;
        _vnPayService = vnPayService;
    }
    
    public async Task<Result<BookingResult>> BookingAsync(BookingRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var seatNames = new List<string>();
            foreach (var seatId in request.SeatId)
            {
                var seat = await _seatRepository.GetSeatAsync(seatId, cancellationToken);
                seatNames.Add(seat.Name);
            }
            
            var filmSchedule = _applicationDbContext.FilmSchedules.Where(x => x.Id == request.ScheduleId);
            var room = _applicationDbContext.Rooms;
            var p1 = filmSchedule.Join(room, x => x.RoomId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var film = _applicationDbContext.Films;
            var p2 = p1.Join(film, x => x.x.FilmId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var theater = _applicationDbContext.Theaters;
            var p3 = p2.Join(theater, x => x.x.y.TheaterId, y => y.Id, (x, y) => new
            {
                x, y   
            }).FirstOrDefault();
            var tableRows = "";
            for (int i = 0; i < seatNames.Count; i++)
            {
                var stt = (i + 1).ToString();
                var tenGhe = seatNames[i];
                var tongTien = request.Total;

                var tableRow = $"<tr><td>{stt}</td><td>{p3.x.y.Name}</td><td>{p3.y.Name}</td><td>{p3.x.x.y.Name}</td><td>{p3.x.x.x.StartTime}</td><td style='font-weight:bold'>{tenGhe}</td><td>{(tongTien / seatNames.Count):N0}đ</td></tr>";
                tableRows += tableRow;
            }

            var body = $@"
            <html>
            <head>
                <style>
                    table {{
                        border-collapse: collapse;
                        width: 100%;
                        border: 1px solid #ddd;
                    }}
                    
                    td, th {{
                        border: 1px solid #ddd;
                        padding: 8px;
                    }}
                    
                    th {{
                        background-color: #f2f2f2;
                    }}
                </style>
            </head>
            <body>
                <table>
                    <tr>
                        <th>STT</th>
                        <th>Tên Phim</th>
                        <th>Rạp Chiếu</th>
                        <th>Phòng Chiếu</th>
                        <th>Thời gian bắt đầu</th>
                        <th>Tên ghế</th>
                        <th>Tổng tiền</th>
                    </tr>
                    {tableRows}
                    <tr>
                        <td></td>
                        <td style='font-weight:bold'>Tổng tiền</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style='font-weight:bold'>{request.Total:N0}đ</td>
                    </tr>
                </table>
            </body>
            </html>";
            _emailService.SendEmail("nva030801@gmail.com", "Test", body);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                return Result<BookingResult>.Succeed();
            }
            return Result<BookingResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(BookingAsync));
            Console.WriteLine(e);
            throw;
        }
    }
}
