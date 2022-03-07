using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected async virtual void OnBefore(IInvocation invocation) { }
        protected async virtual void OnAfter(IInvocation invocation) { }
        protected async virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected async virtual void OnSuccess(IInvocation invocation) { }
        public async override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
                var result = await(Task<string>)invocation.ReturnValue;
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
        }
    }
}
