namespace Automated.Testing.System.DataAccess.Abstractions.Entities
{
    public class Category
    {
        /// <summary>
        /// id категории.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Размер доплаты за разряд.
        /// </summary>
        public int AmountSurcharge { get; set; }
    }
}