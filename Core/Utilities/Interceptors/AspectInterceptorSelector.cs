using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {   //Burda manager'dan istek attığımız methodun ekli methodInterceptionBase Attributeları method ve classtan alıp priority'e göre listeleyip geri döner
        //ona göre bu  aspectleri çağırıyor çağırıken attribute eklediğimiz özelliklerle bu classları oluşturuyor
        //Buna göre sırayla priority'e göre validationAspect,Transcation'u filan çağırıyor
        // birinde onafter diğerinde onbefore varsa nasıl olucak?
        // Bunu recursive bir fonksiyon gibi düşünmek lazım ilk priority inv.proceed dediği zaman 2.sırakadaki araya girip işlemini (intercept) bitiriyor bitince 1 devam ediyor
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes =type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            classAttributes.AddRange(methodAttributes);
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

            return  classAttributes.OrderBy(x=>x.Priority).ToArray();
        }
    }
}
