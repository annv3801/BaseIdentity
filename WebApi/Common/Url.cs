using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591
namespace WebApi.Common
{
    /// <summary>
    /// Manage all urls in this application
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Url
    {
        /// <summary>
        /// Area collections
        /// </summary>
        public static class Areas
        {
            public const string Identity = "Identity";
            public const string Resource = "Resource";
            public const string Tenant = "Tenant";
            public const string DMP = "DMP";
        }

        public static class DMP
        {
            public static class KhuVuc
            {
                public const string ViewList = "DMP/KhuVuc";
            }
            public static class Mien
            {
                public const string ViewList = "DMP/Mien";
            }
            public static class MatHang
            {
                public const string ViewList = "DMP/MatHang";
            }
            public static class NhaHang
            {
                public const string ViewList = "DMP/NhaHang";
            }
            public static class ThuongHieu
            {
                public const string ViewList = "DMP/ThuongHieu";
            }
            public static class Tinh
            {
                public const string ViewList = "DMP/Tinh";
            }
            public static class NhaCungCap
            {
                public const string ViewList = "DMP/NhaCungCap";
            }
            public static class MaterialGroup
            {
                public const string ViewList = "DMP/MaterialGroup";
            }
            public static class Main
            {
                public const string ViewHieuQuaCungUng = "DMP/HieuQuaCungUng";
                public const string ViewLoiLogistics = "DMP/LoiLogistics";
                public const string ViewLoiChatLuong = "DMP/LoiChatLuong";
                public const string ViewLoiChatLuongSanPham = "DMP/LoiChatLuongSanPham";
                public const string ViewSoLuongVaThoiGianGiaoHang = "DMP/SoLuongVaThoiGianGiaoHang";
                public const string ViewListKetQuaChung = "DMP/ViewListKetQuaChung";
                public const string ExportLoiChatLuongSanPham = "DMP/LoiChatLuongSanPham/Export";
                public const string ExportSoLuongVaThoiGianGiaoHang = "DMP/SoLuongVaThoiGianGiaoHang/Export";
                public const string ExportKetQuaChung = "DMP/KetQuaChung/Export";
                public const string DownloadExportFile = "DMP/Download/ExportFile/{fileName}";
                public const string ViewKetQuaChung = "DMP/KetQuaChung";
                public const string ViewKetQuaTheoNhaHang = "DMP/KetQuaTheoNhaHang";
                public const string ViewKetQuaTheoMaHang = "DMP/KetQuaTheoMaHang";
                public const string ViewKetQuaTheoNhaHangPopup = "DMP/KetQuaTheoNhaHang/Popup";
                public const string ExportKetQuaTheoNhaHang = "DMP/KetQuaTheoNhaHang/Export";
                public const string ViewLoiThoiGian = "DMP/LoiThoiGian";
                public const string ExportLoiThoiGian = "DMP/ExportLoiThoiGian/Export";
                public const string ViewLoiSoLuong = "DMP/ViewLoiSoLuong";
                public const string ExportLoiSoLuong = "DMP/ExportLoiSoLuong/Export";
                public const string RankingNcc = "DMP/RankingNcc";
                public const string ExportHieuQuaCungUng = "DMP/HieuQuaCungUng/Export";
                public const string ExportLoiSoLuongAllNcc = "DMP/ExportLoiSoLuongAllNcc/Export";
                public const string ExportLoiThoiGianAllNcc = "DMP/ExportLoiThoiGianAllNcc/Export";
                public const string ExportLoiChatLuongSanPhamAllNcc = "DMP/ExportLoiChatLuongSanPhamAllNcc/Export";
                public const string ExportKetQuaChungAllNcc = "DMP/ExportKetQuaChungAllNcc/Export";
                public const string ExportHieuQuaCungUngByVendor = "DMP/HieuQuaCungUngByVendor/Export";
            }
        }
        public static class Identity
        {
            public static class Account
            {
                public const string Create = "Identity/Account";
                public const string SignInWithPassword = "Identity/Account/SignIn";
                public const string SignInWithOAuth = "Identity/Account/SignInWithOAuth";
                public const string SignInWithPo = "Identity/Account/SignInWithPO";
                public const string ActivateAccount = "Identity/Account/Activation/{accountId}";
                public const string DeactivateAccount = "Identity/Account/Activation/{accountId}";
                public const string Activate = "Identity/Account/Activation";
                public const string Delete = "Identity/Account/{accountId}";
                public const string View = "Identity/Account/{accountId}";
                public const string ViewList = "Identity/Account/List";
                public const string ViewMytAccount = "Identity/Account/MyProfile";
                public const string Lock = "Identity/Account/Lock/{accountId}";
                public const string Unlock = "Identity/Account/Lock/{accountId}";
                public const string UpdateMytAccount = "Identity/Account";
                public const string UpdateAccount = "Identity/Account/{accountId}";
                public const string Logout = "Identity/Account/Logout";
                public const string ForgotMyPassword = "Identity/Account/ForgotPassword";
                public const string ChangePassword = "Identity/Account/ChangePassword";
                public const string SetAccountPassword = "Identity/Account/SetPassword";
                public const string UpdateAccountFcmToken = "Identity/Account/FirebaseToken";
                public const string SetMyNewPassword = "Identity/Account/SetMyNewPassword";
                public const string ChangePasswordAtFirstLogin = "Identity/Account/ChangePasswordFirstLogin";
                public const string SignInWithUserName = "Identity/Account/SignInWithUserName";
            }

            public static class Permission
            {
                public const string Update = "Identity/Permission/{permId}";
                public const string View = "Identity/Permission/{permId}";
                public const string ViewList = "Identity/Permission";
            }

            public static class Role
            {
                public const string Create = "Identity/Role";
                public const string Update = "Identity/Role/{roleId}";
                public const string View = "Identity/Role/{roleId}";
                public const string Delete = "Identity/Role/{roleId}";
                public const string Activate = "Identity/Role/Status/{roleId}";
                public const string Deactivate = "Identity/Role/Status/{roleId}";
                public const string ViewList = "Identity/Role";
            }

            
        }
        public static class Tenant
        {
            public const string Create = "Tenant";
            public const string Update = "Tenant/{tenantId}";
            public const string View = "Tenant/{tenantId}";
            public const string Delete = "Tenant/{tenantId}";
            public const string Activate = "Tenant/Status/{tenantId}";
            public const string Deactivate = "Tenant/Status/{tenantId}";
            public const string ViewList = "Tenant";
        }


        public static class Resource
        {
            public const string UploadFile = "Resource/File";
        }
    }
}