namespace Automated.Testing.System.DataAccess.Abstractions.Entities
{
    /// <summary>
    /// Должность
    /// </summary>
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