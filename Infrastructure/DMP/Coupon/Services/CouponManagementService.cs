using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.Coupon.Commons;
using Application.DMP.Coupon.Queries;
using Application.DMP.Coupon.Repositories;
using Application.DMP.Coupon.Services;
using Application.DMP.Slider.Commons;
using Application.DMP.Slider.Queries;
using Application.DMP.Slider.Repositories;
using Application.DMP.Slider.Services;
using Application.DTO.ActionLog.Requests;
using Application.DTO.DMP.Coupon.Responses;
using Application.DTO.DMP.Slider.Responses;
using Application.DTO.Pagination.Responses;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Coupon.Services;
public class CouponManagementService : ICouponManagementService
{
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICouponRepository _couponRepository;

    public CouponManagementService(ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService, IApplicationDbContext applicationDbContext, ICouponRepository couponRepository)
    {
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
        _applicationDbContext = applicationDbContext;
        _couponRepository = couponRepository;
    }
    public async Task<Result<CouponResult>> CreateCouponAsync(Domain.Entities.DMP.Coupon coupon, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            await _couponRepository.AddAsync(coupon, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Film.Create,
                    Message = LocalizationString.Film.FailedToCreate,
                    MessageParams = new object[] {coupon.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(coupon)
                }, cancellationToken);
                return Result<CouponResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Film.Create,
                Message = LocalizationString.Film.FailedToCreate,
                MessageParams = new object[] {coupon.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(coupon)
            }, cancellationToken);
            return Result<CouponResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateCouponAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Film.Create,
                Message = LocalizationString.Film.FailedToCreate,
                MessageParams = new object[] {coupon.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<ViewCouponResponse>> ViewCouponAsync(Guid couponId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var slider = await _couponRepository.GetCouponAsync(couponId, cancellationToken);
            if (slider == null)
                return Result<ViewCouponResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewCouponResponse>.Succeed(new ViewCouponResponse()
            {
                Id = slider.Id,
                Name = slider.Name, 
                Code = slider.Code,
                Description = slider.Description,
                MinValue = slider.MinValue,
                MaxValue = slider.MaxValue,
                EffectiveStartDate = slider.EffectiveStartDate,
                EffectiveEndDate = slider.EffectiveEndDate,
                Value = slider.Value,
                Quantity = slider.Quantity,
                RemainingQuantity = slider.RemainingQuantity,
                Status = slider.Status
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewCouponAsync));
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewCouponResponse>>> ViewListCouponsAsync(ViewListCouponsQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var keyword = query.Keyword?.ToLower() ?? string.Empty;
            var filterQuery = await _couponRepository.ViewListCouponsAsync(cancellationToken);
            var source = filterQuery.Select(p => new {p.Id, p.Name, p.Code, p.Description, p.MinValue, p.MaxValue, p.EffectiveEndDate, p.EffectiveStartDate, p.Value, p.Quantity, p.RemainingQuantity, p.Status});
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewCouponResponse>>.Fail(
                    _localizationService[LocalizationString.Film.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewCouponResponse>>.Succeed(new PaginationBaseResponse<ViewCouponResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewCouponResponse()
                {
                    Id = a.Id,
                    Name = a.Name, 
                    Code = a.Code,
                    Description = a.Description,
                    MinValue = a.MinValue,
                    MaxValue = a.MaxValue,
                    EffectiveStartDate = a.EffectiveStartDate,
                    EffectiveEndDate = a.EffectiveEndDate,
                    Value = a.Value,
                    Quantity = a.Quantity,
                    RemainingQuantity = a.RemainingQuantity,
                    Status = a.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListCouponsAsync));

            throw;
        }
    }

    public async Task<Result<ViewCouponResponse>> ViewCouponByCodeAsync(string code, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var slider = await _couponRepository.GetCouponByCodeAsync(code, cancellationToken);
            if (slider == null)
                return Result<ViewCouponResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewCouponResponse>.Succeed(new ViewCouponResponse()
            {
                Id = slider.Id,
                Name = slider.Name, 
                Code = slider.Code,
                Description = slider.Description,
                MinValue = slider.MinValue,
                MaxValue = slider.MaxValue,
                EffectiveStartDate = slider.EffectiveStartDate,
                EffectiveEndDate = slider.EffectiveEndDate,
                Value = slider.Value,
                Quantity = slider.Quantity,
                RemainingQuantity = slider.RemainingQuantity,
                Status = slider.Status
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewCouponByCodeAsync));
            throw;
        }
    }
}
