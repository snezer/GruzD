using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GruzD.Web.Infrastructure.Security
{
    public static class KnownClaims
    {
        public const string ClaimType = "TOIR";

        static KnownClaims()
        {
            Claims = typeof(KnownClaims)
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(field => field.IsLiteral)
                .ToDictionary(
                    field => field.Name,
                    field => field.GetValue(null).ToString()
                 );
        }

        public const string ObjectCreate = "Object.Create";
        public const string ObjectEdit = "Object.Edit";
        public const string ObjectView = "Object.View";
        public const string ObjectViewAll = "Object.ViewAll";

        public const string TaskCreate = "Task.Create";
        public const string TaskEdit = "Task.Edit";
        public const string TaskView = "Task.View";
        public const string TaskViewAll = "Task.ViewAll";

        public static Dictionary<string, string> Claims { get; }
    }
}
