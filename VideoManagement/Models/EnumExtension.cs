using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoManagement.Models
{
    public static class EnumExtension
    {
        public static List<TEntity> ToList<TEntity>(Type enumType) where TEntity : EnumItem, new()
        {
            var ls = Enum.GetValues(enumType).OfType<Enum>().ToList().Select(a => new TEntity()
            {
                Id = (int)((object)a),
                Text = a.GetDisplayName()
            }).ToList();

            return ls;
        }
    }
}
