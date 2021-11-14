namespace Automated.Testing.System.DataAccess.Postgres.Entities
{
    public sealed class Post
    {
        /// <summary>
        /// id должности.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }
    }
}