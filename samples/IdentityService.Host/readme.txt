一、数据迁移
1. add-migration InitApplication -context ApplicationDbContext
2. add-migration InitPersistedGrant -context PersistedGrantDbContext
3. add-migration InitConfiguration -context ConfigurationDbContext

update-database -context ApplicationDbContext
update-database -context PersistedGrantDbContext
update-database -context ConfigurationDbContext