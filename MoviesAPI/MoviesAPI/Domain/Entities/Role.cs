namespace MoviesAPI.Domain.Entities
{
    public class Role
    {
        /// <summary>
        /// Id del rol
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del rol
        /// </summary>
        public string Name { get; set; } = null!;
    }
}
