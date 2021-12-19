using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Extensions
{
    public static class SetValue
    {
        public static T Transaction<T>(T objeto, string propertyName, object value)
        {
            var prop = objeto.GetType().GetProperties().Where(p => p.Name == propertyName).FirstOrDefault();
            prop.SetValue(objeto, value);
            return objeto;
        }
    }
}
