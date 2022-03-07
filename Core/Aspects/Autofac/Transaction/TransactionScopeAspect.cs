using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        //ef kendi içinde kullandığı uow patterni bunu kullanıyor

        //transaction başında sonunda yerine bir döngü methodun yaşam döngüsü lazım o yüzden override edip baştan yazıyoruz
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();
                    throw e;
                }
            }
        }
    }
}
