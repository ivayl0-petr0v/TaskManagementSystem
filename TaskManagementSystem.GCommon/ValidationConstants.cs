using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.GCommon
{
    public static class ValidationConstants
    {   
        //Status
        public const int StatusNameMaxLength = 30;
        //Category
        public const int CategoryNameMaxLength = 50;
        //Project
        public const int ProjectTitleMinLength = 5;
        public const int ProjectTitleMaxLength = 100;
        public const int ProjectDescriptionMinLength = 10;
        public const int ProjectDescriptionMaxLength = 1000;
        //ApplicationUser
        public const int AppUserFirstNameMaxLength = 100;
        public const int AppUserLastNameMaxLength = 100;
    }
}
