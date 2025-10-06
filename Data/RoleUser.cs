using Microsoft.AspNetCore.Identity;
using SCoffee.Models;

namespace SCoffee.Data
{
    public class RoleUser
    {
        public static async Task Createdefaultvalue(IServiceProvider service)
        {
            var manageUser = service.GetService<UserManager<IdentityUser>>();
            var manageRole = service.GetService<RoleManager<IdentityRole>>();

            // Thêm một vai trò trong cơ sở dữ liệu
            await manageRole.CreateAsync(new IdentityRole(Role.Admin.ToString()));
            await manageRole.CreateAsync(new IdentityRole(Role.User.ToString()));

            // Tạo tài khoản mặc định cho Admin
            var Admin = new IdentityUser
            {
                UserName = "Admin123@gmail.com",
                Email = "Admin123@gmail.com",
                EmailConfirmed = true,
            };
            var UserInDB = await manageUser.FindByEmailAsync(Admin.Email);
            
            // Nếu tài khoản Admin không tồn tại
            if (UserInDB is null)
            {
                //Tạo tài khoản Admin
                await manageUser.CreateAsync(Admin, "Admin@123");
                await manageUser.AddToRoleAsync(Admin, Role.Admin.ToString());
            }
        }
    }
}
