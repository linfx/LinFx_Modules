namespace Identity.Application
{
    public static class Permissions
    {
        public const string GroupName = "Identity";

        public static class Role
        {
            public const string Default = GroupName + ".Role";
            public const string Index = Default + ".Index";
            public const string Delete = Default + ".Delete";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
            public const string Details = Default + ".Details";
        }

        public static class User
        {
            public const string Default = GroupName + ".User";
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

                Role.Default,
                Role.Index,
                Role.Delete,
                Role.Edit,
                Role.Create,
                Role.Details,

                User.Default,
                User.Index,
                User.Delete,
                User.Edit,
                User.Create,
                User.Details,
            };
        }
    }
}
