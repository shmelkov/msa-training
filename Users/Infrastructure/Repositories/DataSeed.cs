using Microsoft.EntityFrameworkCore;
//using Portal.Common.Constants;
using Users.Core.Entities;

namespace Users.Infrastructure.Repositories
{
    public class DataSeed
    {
        //public static void SeedUsersModuleSetting(ModelBuilder modelBuilder)
        //{
        //   // modelBuilder.Entity<UsersModuleSetting>().HasData(
        //   //    new UsersModuleSetting
        //   //    {
        //   //        Id = Guid.NewGuid(),
        //   //        EmployeesCardsNumber = 10,
        //   //        OrgPositionsCardsNumber = 8,
        //   //        ShowEmptySubdivisions = false,
        //   //        TermsOfUse = ""
        //   //    }
        //   //);
        //}

        //public static void SeedRolesAndModules(ModelBuilder modelBuilder)
        //{
        //    //var moduleIds = new Guid[15]
        //    //{
        //    //    Guid.Parse("bfa631c2-d0ae-4bb8-a8b7-8e8795d9287e"),
        //    //    Guid.Parse("7d0393c7-a186-4d67-ae4e-ed4ac7aa259e"),
        //    //    Guid.Parse("8f99a584-b118-483e-bf73-805d02083a9f"),
        //    //    Guid.Parse("06417ca2-6081-4ac4-a225-0f0320031682"),
        //    //    Guid.Parse("dec65125-4980-4177-ba2d-a181a6174ab9"),
        //    //    Guid.Parse("0c770525-4ec2-4fae-966c-602a031e3e7a"),
        //    //    Guid.Parse("597a1157-4ad5-4327-a121-24d62b21613e"),
        //    //    Guid.Parse("d0c43c8c-baa7-4108-b73a-d42507510af6"),
        //    //    Guid.Parse("d88ea181-d309-4e06-8ce5-d7aff4eb909d"),
        //    //    Guid.Parse("64699dd0-eda4-48ad-8a36-bc246b31ce29"),
        //    //    Guid.Parse("773c05dd-e655-4ffb-8d5e-bea3d91fcd07"),
        //    //    Guid.Parse("5e7c61b2-6d2f-4ffe-88d1-b233dc8a9e0a"),
        //    //    Guid.Parse("8a0f1e70-af4d-442b-9ec1-3e7c38342ce4"),
        //    //    Guid.Parse("8c85df3f-bd61-4da4-8f18-fd78d412156d"),
        //    //    Guid.Parse("30def026-4200-4647-b13e-4749f687477d")
        //    //};

        //    //var modules = new[]
        //    //{
        //    //    new ApplicationModule { Id = moduleIds[0], Title = "Профиль"},
        //    //    new ApplicationModule { Id = moduleIds[1], Title = "Справочник сотрудников"},
        //    //    new ApplicationModule { Id = moduleIds[2], Title = "Навигация"},
        //    //    new ApplicationModule { Id = moduleIds[3], Title = "Техническая поддержка"},
        //    //    new ApplicationModule { Id = moduleIds[4], Title = "Новости"},
        //    //    new ApplicationModule { Id = moduleIds[5], Title = "Организационная структура"},
        //    //    new ApplicationModule { Id = moduleIds[6], Title = "Информационные сообщения"},
        //    //    new ApplicationModule { Id = moduleIds[7], Title = "Баннеры"},
        //    //    new ApplicationModule { Id = moduleIds[8], Title = "Благодарности"},
        //    //    new ApplicationModule { Id = moduleIds[9], Title = "Дни Рождения"},
        //    //    new ApplicationModule { Id = moduleIds[10], Title = "Сообщества"},
        //    //    new ApplicationModule { Id = moduleIds[11], Title = "Опросы"},
        //    //    new ApplicationModule { Id = moduleIds[12], Title = "Календарь событий"},
        //    //    new ApplicationModule { Id = moduleIds[13], Title = "Библиотека документов"},
        //    //    new ApplicationModule { Id = moduleIds[14], Title = "Оповещения"},
        //    //};

        //    //modelBuilder.Entity<ApplicationModule>().HasData(modules);

        //    //var roles = new List<ApplicationRole>();

        //    //foreach (var module in modules)
        //    //{
        //    //    roles.Add(new ApplicationRole { Id = Guid.NewGuid(), DisplayName = GetFullRoleName(module.Title, Roles.User), Name = GetFullRoleNameEng(module.Title, Roles.UserEng), NormalizedName = GetFullRoleNameEng(module.Title, Roles.UserEng).ToUpper(), ModuleId = module.Id });
        //    //    roles.Add(new ApplicationRole { Id = Guid.NewGuid(), DisplayName = GetFullRoleName(module.Title, Roles.Editor), Name = GetFullRoleNameEng(module.Title, Roles.EditorEng), NormalizedName = GetFullRoleNameEng(module.Title, Roles.EditorEng).ToUpper(), ModuleId = module.Id });
        //    //    roles.Add(new ApplicationRole { Id = Guid.NewGuid(), DisplayName = GetFullRoleName(module.Title, Roles.Admin), Name = GetFullRoleNameEng(module.Title, Roles.AdminEng), NormalizedName = GetFullRoleNameEng(module.Title, Roles.AdminEng).ToUpper(), ModuleId = module.Id });         
        //    //}

        //    //modelBuilder.Entity<ApplicationRole>().HasData(roles);
        //}

        private static string GetFullRoleName(string moduleName, string roleName)
        {
            return moduleName + " - " + roleName;
        }

        //private static string GetFullRoleNameEng(string moduleName, string roleName)
        //{
        //    string translatedName = "";
        //    switch (moduleName)
        //    {
        //        case "Профиль":
        //            translatedName = "Profile";
        //            break;
        //        case "Справочник сотрудников":
        //            translatedName = "CorpBook";
        //            break;
        //        case "Навигация":
        //            translatedName = "Navigation";
        //            break;
        //        case "Техническая поддержка":
        //            translatedName = "TechSupport";
        //            break;
        //        case "Новости":
        //            translatedName = "News";
        //            break;
        //        case "Организационная структура":
        //            translatedName = "OrgStructure";
        //            break;
        //        case "Информационные сообщения":
        //            translatedName = "PopUp";
        //            break;
        //        case "Баннеры":
        //            translatedName = "Banners";
        //            break;
        //        case "Благодарности":
        //            translatedName = "Thanks";
        //            break;
        //        case "Дни Рождения":
        //            translatedName = "Birthdays";
        //            break;
        //        case "Сообщества":
        //            translatedName = "Communities";
        //            break;
        //        case "Опросы":
        //            translatedName = "Surveys";
        //            break;
        //        case "Календарь событий":
        //            translatedName = "EventCalendar";
        //            break;
        //        case "Библиотека документов":
        //            translatedName = "DocumentLibrary";
        //            break;
        //        case "Оповещения":
        //            translatedName = "Notifications";
        //            break;
        //        default:
        //            translatedName = "";
        //            break;
        //    }

        //    return translatedName + roleName;
        //}


        //public static void SeedEmployeeLinks(ModelBuilder modelBuilder)
        //{
        //    var employeeIds = new Guid[3]
        //    {
        //        Guid.Parse("a1d04d2a-3bba-474a-870b-b130d8c99a3a"),
        //        Guid.Parse("83b18ff1-0949-4dd3-ac4e-1ed2ffa66781"),
        //        Guid.Parse("ed5bc93c-b978-40ea-bd0b-bde1c3c21ab9")
        //    };
        //    var ids = new Guid[3]
        //    {
        //        Guid.Parse("df18092e-97e9-4073-913f-df2ef94d4850"),
        //        Guid.Parse("fd3a61b8-15fb-4438-aa35-ab70b573548c"),
        //        Guid.Parse("1cb307e2-8c63-4a86-b06d-fb3653021e77")
        //    };
        //    var companyIds = new Guid[3]
        //    {
        //        Guid.Parse("a634519a-0cb9-47a7-adc1-78f1af4639f1"),
        //        Guid.Parse("cbfa733a-03b4-4b7a-8fea-14d7b06682c9"),
        //        Guid.Parse("f63122aa-07be-4d14-ae3e-7378b9942bcf")
        //    };

        //    modelBuilder.Entity<Company>().HasData(new[]
        //    {
        //        new Company { Id = companyIds[0], Title = "Ауксо" },
        //        new Company { Id = companyIds[1], Title = "Норникель" },
        //        new Company { Id = companyIds[2], Title = "Перфетти" },
        //    });

        //    modelBuilder.Entity<Employee>().HasData(new[]
        //    {
        //        new Employee { Id = employeeIds[0], FirstName = "Даниил", MiddleName = "Иванович", LastName = "Краснодуб", UserName="int\\A835349", Email = "daniil@mail.ru", Birthdate = new DateTime(2000, 7, 20).ToUniversalTime(), EmploymentDate = new DateTime(2022, 6, 19).ToUniversalTime(), PersonnelNumber="00011823", Location="Воронеж, пр. Труда, 65",  WorkPhone="+7(473)241-52-11", MobilePhone = "+7(987)654-32-10", CompanyId = companyIds[0] },
        //        new Employee { Id = employeeIds[1], FirstName = "Сергей", MiddleName = "Сергеевич", LastName = "Иванов", UserName="int\\A412328", Email = "sergey@pochta.ru", Birthdate = new DateTime(1980, 3, 24).ToUniversalTime(), EmploymentDate = new DateTime(2019, 9, 22).ToUniversalTime(), PersonnelNumber="00010826", Location="Воронеж, пр. Труда, 65", WorkPhone="+7(916)231-14-52", MobilePhone = "+7(851)167-80-19", CompanyId = companyIds[1] },
        //        new Employee { Id = employeeIds[2], FirstName = "Михаил", MiddleName = "Владимирович", LastName = "Гагарин", UserName="int\\A712323", Email = "michail@box.com", Birthdate = new DateTime(2010, 10, 16).ToUniversalTime(), EmploymentDate = new DateTime(2023, 1, 12).ToUniversalTime(), PersonnelNumber="00020820", Location="Воронеж, пр. Труда, 65", WorkPhone="+7(916)231-22-70", MobilePhone = "+7(688)244-45-98", CompanyId = companyIds[2] }
        //    });

        //    modelBuilder.Entity<LinkCategory>().HasData(new[]
        //    {
        //        new LinkCategory { Id = ids[0], Title = "Разное", IsActive = true },
        //        new LinkCategory { Id = ids[1], Title = "Важное", IsActive = false },
        //        new LinkCategory { Id = ids[2], Title = "Обязательное", IsActive = true }
        //    });

        //    modelBuilder.Entity<EmployeeLink>().HasData(new[]
        //    {
        //        new EmployeeLink { Id = Guid.NewGuid(),  Title = "Ссылка №1", Url = "хттп://ссылка1", EmployeeId = employeeIds[0], LinkCategoryId = ids[2]},
        //        new EmployeeLink { Id = Guid.NewGuid(),  Title = "Ссылка №2", Url = "хттп://ссылка2", EmployeeId = employeeIds[2], LinkCategoryId = ids[1]},
        //        new EmployeeLink { Id = Guid.NewGuid(),  Title = "Ссылка №3", Url = "хттп://ссылка3", EmployeeId = employeeIds[1], LinkCategoryId = ids[0]},
        //    });
        //}

        //public static void SeedEmployeeSkillsAndHobbies(ModelBuilder modelBuilder)
        //{
        //    var departmentIds = new Guid[7]
        //    {
        //        Guid.Parse("df18092e-97e9-4073-913f-df2ef94d4850"),
        //        Guid.Parse("ed5bc93c-b978-40ea-bd0b-bde1c3c21ab9"),
        //        Guid.Parse("1cb307e2-8c63-4a86-b06d-fb3653021e77"),
        //        Guid.Parse("fd3a61b8-15fb-4438-aa35-ab70b573548c"),
        //        Guid.Parse("a1d04d2a-3bba-474a-870b-b130d8c99a3a"),
        //        Guid.Parse("83b18ff1-0949-4dd3-ac4e-1ed2ffa66781"),
        //        Guid.Parse("be7a70fd-38e4-478c-9f15-69a4dda21840")
        //    };
        //    var employeeIds = new Guid[3]
        //    {
        //        Guid.Parse("a1d04d2a-3bba-474a-870b-b130d8c99a3a"),
        //        Guid.Parse("83b18ff1-0949-4dd3-ac4e-1ed2ffa66781"),
        //        Guid.Parse("ed5bc93c-b978-40ea-bd0b-bde1c3c21ab9")
        //    };
        //    var ids = new Guid[3]
        //    {
        //        Guid.Parse("df18092e-97e9-4073-913f-df2ef94d4850"),
        //        Guid.Parse("fd3a61b8-15fb-4438-aa35-ab70b573548c"),
        //        Guid.Parse("1cb307e2-8c63-4a86-b06d-fb3653021e77")
        //    };
        //    var hobbyIds = new Guid[3]
        //    {
        //        Guid.Parse("a122ecf8-eaf6-42d6-a601-148abd7178c9"),
        //        Guid.Parse("f163c65a-8898-4c27-bc77-fce5bc563dc4"),
        //        Guid.Parse("1318fc04-d7c1-450f-8a54-db64e23fc6d0")
        //    };
        //    modelBuilder.Entity<Skill>().HasData(new[]
        //    {
        //        new Skill { Id = ids[0], Title = "Python", IsActive = true },
        //        new Skill { Id = ids[1], Title = "React", IsActive = false },
        //        new Skill { Id = ids[2], Title = "SAP", IsActive = true }
        //    });

        //    modelBuilder.Entity<EmployeeSkill>().HasData(new[]
        //    {
        //        new EmployeeSkill { Id = Guid.NewGuid(), EmployeeId = employeeIds[0], SkillId = ids[2]},
        //        new EmployeeSkill { Id = Guid.NewGuid(), EmployeeId = employeeIds[2], SkillId = ids[1]},
        //        new EmployeeSkill { Id = Guid.NewGuid(), EmployeeId = employeeIds[1], SkillId = ids[0]},
        //    });

        //    modelBuilder.Entity<Hobby>().HasData(new[]
        //    {
        //        new Hobby { Id = hobbyIds[0], Title = "Черчение", IsActive = true},
        //        new Hobby { Id = hobbyIds[1], Title = "Вышивание", IsActive = true },
        //        new Hobby { Id = hobbyIds[2], Title = "Настольные игры", IsActive = true }
        //    });

        //    modelBuilder.Entity<EmployeeHobby>().HasData(new[]
        //    {
        //        new EmployeeHobby { Id = Guid.NewGuid(), EmployeeId = employeeIds[0], HobbyId = hobbyIds[0]},
        //        new EmployeeHobby { Id = Guid.NewGuid(), EmployeeId = employeeIds[0], HobbyId = hobbyIds[1]},
        //        new EmployeeHobby { Id = Guid.NewGuid(), EmployeeId = employeeIds[1], HobbyId = hobbyIds[1]},
        //        new EmployeeHobby { Id = Guid.NewGuid(), EmployeeId = employeeIds[1], HobbyId = hobbyIds[2]},
        //        new EmployeeHobby { Id = Guid.NewGuid(), EmployeeId = employeeIds[2], HobbyId = hobbyIds[2]},
        //        new EmployeeHobby { Id = Guid.NewGuid(), EmployeeId = employeeIds[2], HobbyId = hobbyIds[0]},
        //    });

        //    modelBuilder.Entity<Department>().HasData(new[]
        //    {
        //        new Department{ Id = departmentIds[0], ManagerId = employeeIds[0], Code = "123", IsHidden = false, Title = "Department 11", ShortTitle = "d1"},
        //        new Department{ Id = departmentIds[1], ManagerId = employeeIds[0], Code = "132", IsHidden = false, Title = "Dept X2", ShortTitle = "d12", ParentId = departmentIds[0]},
        //        new Department{ Id = departmentIds[2], ManagerId = employeeIds[1], Code = "231", IsHidden = false, Title = "Room 32", ShortTitle = "x14", ParentId = departmentIds[1]},
        //        new Department{ Id = departmentIds[3], ManagerId = employeeIds[1], Code = "213", IsHidden = true, Title = "Department 41", ShortTitle = "r17", ParentId = departmentIds[2]},
        //        new Department{ Id = departmentIds[4], ManagerId = employeeIds[2], Code = "323", IsHidden = false, Title = "Department 5", ShortTitle = "DP75", ParentId = departmentIds[3]},
        //        new Department{ Id = departmentIds[5], ManagerId = employeeIds[2], Code = "321", IsHidden = false, Title = "Dept M64", ShortTitle = "DP64", ParentId = departmentIds[4]},
        //        new Department{ Id = departmentIds[6], ManagerId = employeeIds[0], Code = "312", IsHidden = true, Title = "Department 7", ShortTitle = "DPT7", ParentId = departmentIds[5]},
        //    });

        //    modelBuilder.Entity<Position>().HasData(new[]
        //    {
        //        new Position { Id = Guid.NewGuid(), Code = "123", IsMain = true, Title = "Руководитель проекта",  EmployeeId = employeeIds[0], DepartmentId = departmentIds[0]},
        //        new Position { Id = Guid.NewGuid(), Code = "123", IsMain = false, Title = "Руководитель группы",  EmployeeId = employeeIds[0], DepartmentId = departmentIds[0]},
        //        new Position { Id = Guid.NewGuid(), Code = "123", IsMain = true, Title = "Эксперт информационных технологий",  EmployeeId = employeeIds[1], DepartmentId = departmentIds[0]},
        //        new Position { Id = Guid.NewGuid(), Code = "123", IsMain = false, Title = "Экономист",  EmployeeId = employeeIds[1], DepartmentId = departmentIds[1]},
        //        new Position { Id = Guid.NewGuid(), Code = "123", IsMain = true, Title = "Программист",  EmployeeId = employeeIds[2], DepartmentId = departmentIds[1]},
        //    });

        //    modelBuilder.Entity<EmployeeSettings>().HasData(new[]
        //    {
        //        new EmployeeSettings { Id = Guid.NewGuid(),  EmployeeId = employeeIds[0], IsMobilePhoneHidden = true, IsBirthdayHidden = false, IsBirthYearHidden = false},
        //        new EmployeeSettings { Id = Guid.NewGuid(),  EmployeeId = employeeIds[1], IsMobilePhoneHidden = false, IsBirthdayHidden = false, IsBirthYearHidden = false},
        //        new EmployeeSettings { Id = Guid.NewGuid(),  EmployeeId = employeeIds[2], IsMobilePhoneHidden = false, IsBirthdayHidden = true, IsBirthYearHidden = true}
        //    });
        //}

        //public static void SeedProfileSettings(ModelBuilder builder)
        //{
        //    builder.Entity<ProfileSetting>().HasData(
        //       new ProfileSetting
        //       {
        //           Id = Guid.NewGuid(),
        //           PhotoPostingRules = "Требования к фото",
        //           ThanksFromMeCardsCount = 2,
        //           MyCongratulationsCardsCount = 3,
        //           MyCommunitiesCardsCount = 6,
        //           MyCalendarCardsCount = 3,
        //           IsShowBanner = false
        //       }
        //   );
        //}

        internal static void SeedDefaultUsersAndGroups(ModelBuilder modelBuilder)
        {
            var adminUserId = Guid.Parse("73e6fa95-ac71-48b5-bc07-52db8c3ce4e8");
            var adminGroupId = Guid.Parse("2b0f0f6d-f27a-43e5-93a6-095f470c70f8");
            var adminRoleId = Guid.Parse("556efeed-20a2-4187-82d4-76a18b5bcc40");

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = adminUserId,
                UserName = "demoapp",
                NormalizedUserName = "DEMOAPP",
                FirstName = "DEMOAPP",
                LastName = "ADMIN",
                MiddleName = "",
                Modified = DateTime.UtcNow,
                Created = DateTime.UtcNow
            });

            modelBuilder.Entity<ApplicationGroup>().HasData(new ApplicationGroup { Id = adminGroupId, Title = "Администраторы", Description = "desc" });

            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole { Id = adminRoleId, DisplayName = "Администратор портала", Name = "DemoAppAdmin", NormalizedName = "DEMOAPPADMIN" });

            modelBuilder.Entity<ApplicationGroupRole>().HasData(new ApplicationGroupRole { GroupId = adminGroupId, RoleId = adminRoleId, Id = Guid.NewGuid() });

            modelBuilder.Entity<ApplicationUserGroup>().HasData(new ApplicationUserGroup { GroupId = adminGroupId, UserId = adminUserId, Id = Guid.NewGuid() });
        }
    }
}
