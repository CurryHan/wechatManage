using System;
using System.ComponentModel.DataAnnotations;
using rs = Resources.Resource;
namespace Sensing.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = nameof(rs.HtmlTemplate), ResourceType = typeof(rs))]
        public DateTime? LastUpdated { get; set; }

        [Display(Name = "Updater", ResourceType = typeof(Resources.Resources))]
        public string Updater { get; set; }

        /// <summary>
        /// the time item was created.
        /// </summary>
        [Display(Name = "Created", ResourceType = typeof(Resources.Resources))]
        public DateTime? Created { get; set; }

        [Display(Name = "Creator", ResourceType = typeof(Resources.Resources))]
        public string Creator { get; set; }

        public bool Deleted { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Resources))]
        public string Description { get; set; }

        #region Options for extension.
        //gaps for ui display in order.
        public int OrderNumber { get; set; }

        public string Comments { get; set; }

        public string Extends { get; set; }

        #endregion

        public virtual bool IsTransient()
        {
            return this.Id == default(int);
        }
    }
}
