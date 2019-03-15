namespace LinFx.Identity.Application
{
    public static class IdentityPermissions
    {
        public const string GroupName = "Identity";

        public static class Roles
        {
            public const string Default = GroupName + ".Roles";
            public const string Index = Default + ".Index";
            public const string Delete = Default + ".Delete";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
            public const string Details = Default + ".Details";
        }

        public static class Users
        {
            public const string Default = GroupName + ".Users";
            public const string Index = Default + ".Index";
            public const string Delete = Default + ".Delete";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
            public const string Details = Default + ".Details";
        }

        public static string[] GetAll()
        {
            return new[]
            {
                GroupName,

                Roles.Default,
                Roles.Index,
                Roles.Delete,
                Roles.Edit,
                Roles.Create,
                Roles.Details,

                Users.Default,
                Users.Index,
                Users.Delete,
                Users.Edit,
                Users.Create,
                Users.Details,
            };
        }
    }
}
