using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591
namespace WebApi.Common
{
    /// <summary>
    /// To manage features in this application
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Feature
    {
        public static class Modules
        {
            public const string AccountManagement = "AccountManagement";
            public const string ResourceManagement = "ResourceManagement";
            public const string PermissionManagement = "PermissionManagement";
            public const string RoleManagement = "RoleManagement";
            public const string TenantManagement = "TenantManagement";
            public const string DMP = "DMP";
        }

        public static class Functions
        {
            public static class DMP
            {
                public static class KhuVuc
                {
                    public const string ViewList = "DMP|KhuVuc|ViewList";
                }
                public static class Mien
                {
                    public const string ViewList = "DMP|Mien|ViewList";
                }
                public static class MatHang
                {
                    public const string ViewList = "DMP|MatHang|ViewList";
                }
                public static class NhaHang
                {
                    public const string ViewList = "DMP|NhaHang|ViewList";
                }
                public static class ThuongHieu
                {
                    public const string ViewList = "DMP|ThuongHieu|ViewList";
                }
                public static class Tinh
                {
                    public const string ViewList = "DMP|Tinh|ViewList";
                }
                public static class NhaCungCap
                {
                    public const string ViewList = "DMP|NhaCungCap|ViewList";
                }
                public static class MaterialGroup
                {
                    public const string ViewList = "DMP|MaterialGroup|ViewList";
                }
                public static class Main
                {
                    public const string ViewHieuQuaCungUng = "DMP|Main|ViewHieuQuaCungUng";
                    public const string ViewLoiLogistics = "DMP|Main|ViewLoiLogistics";
                    public const string ViewLoiChatLuong = "DMP|Main|ViewLoiChatLuong";
                    public const string ViewLoiChatLuongSanPham = "DMP|Main|ViewLoiChatLuongSanPham";
                    public const string ViewSoLuongVaThoiGianGiaoHang = "DMP|Main|ViewSoLuongVaThoiGianGiaoHang";
                    public const string ViewListKetQuaChung = "DMP|Main|ViewListKetQuaChung";
                    public const string ExportLoiChatLuongSanPham = "DMP|Main|ExportLoiChatLuongSanPham";
                    public const string ExportSoLuongVaThoiGianGiaoHang = "DMP|Main|ExportSoLuongVaThoiGianGiaoHang";
                    public const string ExportKetQuaChung = "DMP|Main|ExportKetQuaChung";
                    public const string ExportKetTheoNhaHang = "DMP|Main|ExportKetTheoNhaHang";
                    public const string DownloadExportFile = "DMP|Main|DownloadExportFile";
                    public const string ViewKetQuaChung = "DMP|Main|ViewKetQuaChung";
                    public const string ViewKetQuaTheoNhaHang = "DMP|Main|ViewKetQuaTheoNhaHang";
                    public const string ViewKetQuaTheoMaHang = "DMP|Main|ViewKetQuaTheoMaHang";
                    public const string ViewKetQuaTheoMaHangPopup = "DMP|Main|ViewKetQuaTheoMaHangPopup";
                    public const string ViewLoiThoiGian = "DMP|Main|ViewLoiThoiGian";
                    public const string ExportLoiThoiGian = "DMP|Main|ExportLoiThoiGian";
                    public const string ViewLoiSoLuong = "DMP|Main|ViewLoiSoLuong";
                    public const string ExportLoiSoLuong = "DMP|Main|ExportLoiSoLuong";
                    public const string RankingNcc = "DMP|Main|RankingNcc";
                    public const string ExportHieuQuaCungUng = "DMP|Main|ExportHieuQuaCungUng";
                    public const string ExportLoiSoLuongAllNcc = "DMP|Main|ExportLoiSoLuongAllNcc";
                    public const string ExportLoiThoiGianAllNcc = "DMP|Main|ExportLoiThoiGianAllNcc";
                    public const string ExportLoiChatLuongSanPhamAllNcc = "DMP|Main|ExportLoiChatLuongSanPhamAllNcc";
                    public const string ExportKetQuaChungAllNcc = "DMP|Main|ExportKetQuaChungAllNcc";
                    public const string ExportHieuQuaCungUngByVendor = "DMP|Main|ExportHieuQuaCungUngByVendor";
                }
            }
            public static class Identity
            {
                public static class Account
                {
                    public const string SignInWithPhoneNumber = "Identity|Account|SignInWithPhoneNumber";
                    public const string SignInWithOAuth = "Identity|Account|SignInWithOAuth";
                    public const string SignInWithPo = "Identity|Account|SignInWithPO";
                    public const string SignInWithUserName = "Identity|Account|SignInWithUserName";
                    public const string ForgotMyPassword = "Identity|Account|ForgotMyPassword";
                    public const string ActivateAccount = "Identity|Account|Activation";
                    public const string UpdateMyAccount = "Identity|Account|UpdateMyAccount";
                    public const string Logout = "Identity|Account|Logout";
                    public const string ChangeMyPassword = "Identity|Account|ChangeMyPassword";
                    public const string UpdateAccountFcmToken = "Identity|Account|UpdateAccountFCMToken";
                    public const string ViewMyAccount = "Identity|Account|ViewMyAccount";
                    public const string ViewList = "Identity|Account|ViewList";
                    public const string Create = "Identity|Account|Create";
                    public const string View = "Identity|Account|View";
                    public const string Lock = "Identity|Account|Lock";
                    public const string Unlock = "Identity|Account|Unlock";
                    public const string SetPassword = "Identity|Account|SetAccountPassword";
                    public const string Activate = "Identity|Account|Activate";
                    public const string Deactivate = "Identity|Account|Deactivate";
                    public const string Delete = "Identity|Account|Delete";
                    public const string Update = "Identity|Account|Update";
                    public const string SetMyNewPassword = "Identity|Account|SetMyNewPassword";

                    public const string ChangePasswordAtFirstLogin = "Identity|Account|ChangePasswordAtFirstLogin";
                }

                public static class Permission
                {
                    public const string Create = "Identity|Permission|Create";
                    public const string Update = "Identity|Permission|Update";
                    public const string View = "Identity|Permission|View";
                    public const string ViewList = "Identity|Permission|ViewList";
                }

                public static class Role
                {
                    public const string Create = "Identity|Role|Create";
                    public const string Update = "Identity|Role|Update";
                    public const string View = "Identity|Role|View";
                    public const string Delete = "Identity|Role|Delete";
                    public const string Activate = "Identity|Role|Activate";
                    public const string Deactivate = "Identity|Role|Deactivate";
                    public const string ViewList = "Identity|Role|ViewList";
                }
            }

            public static class Tenant
            {
                public const string Create = "Tenant|Tenant|Create";
                public const string Update = "Tenant|Tenant|Update";
                public const string View = "Tenant|Tenant|View";
                public const string Delete = "Tenant|Tenant|Delete";
                public const string Activate = "Tenant|Tenant|Activate";
                public const string Deactivate = "Tenant|Tenant|Deactivate";
                public const string ViewList = "Tenant|Tenant|ViewList";
            }

            public static class Resource
            {
                public static class File
                {
                    public const string UploadFile = "Resource|File|UploadFile";
                }
            }
        }
    }
}