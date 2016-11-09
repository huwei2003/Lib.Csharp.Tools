using System;
using System.Diagnostics;
using System.Transactions;

namespace Lib.Csharp.Tools
{
    public static class TransactionHelper
    {
        /// <summary>
        /// 事务选项：事务级别为ReadCommitted，2分钟事务超时
        /// </summary>
        internal static readonly TransactionOptions TransactionOption = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = new TimeSpan(0, 2, 0)
        };
        /// <summary>
        /// 执行时间警告毫秒数
        /// </summary>
        internal const int WornigMilliseconds = 2000;

        /// <summary>
        /// 复杂事务更新(支持多表更新保持事务，支持跨数据库事务，支持C#函数事务)
        /// <para>例：更新用户表 + 江苏票表 + 批量sql</para>
        /// <para>new Action(() =></para>
        /// <para>{</para>
        /// <para>　　　new List&lt;T_User>().UpdateNoTrans(a => "id=" + a.Id);</para>
        /// <para>　　　new List&lt;TB_LOTTERY_INFO>().UpdateNoTrans(a => "id=" + a.DRAW_FLAG);</para>
        /// <para>　　　Sql3.ExecuteNoTrans("");</para>
        /// <para>}).ExecuteTrans();</para>
        /// <para>例：C#函数使用事务</para>
        /// <para>1、无参数的函数</para>
        /// <para>new Action(AddUser).ExecuteTrans();</para>
        /// <para>2、带参数的函数</para>
        /// <para> new Action(() =></para>
        /// <para> {</para>
        /// <para>　　　var id = 1;</para>
        /// <para>　　　AddUser(id);</para>
        /// <para> }).ExecuteTrans();</para>
        /// <para> TransactionHelper.ExecuteTrans(() =></para>
        /// <para> {</para>
        /// <para>　　　AddUser(1, 1);</para>
        /// <para> });</para>
        /// <para>注：事务内部调用函数不允许有子事务存在(C#函数需无try catch，否则不能保证事务完整性)，例中均为NoTrans</para>
        /// </summary>
        public static bool ExecuteTrans(this Action action)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                using (var trans = new TransactionScope(TransactionScopeOption.Required, TransactionOption))
                {
                    action();
                    trans.Complete();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
            finally
            {
                var logStr = "{0}(action) {1}".Formats(action.Method.Name, sw.Elapsed.ToUseSimpleTime());
                if (sw.ElapsedMilliseconds > WornigMilliseconds)
                {
                    Log.Warn(logStr);
                }
                Log.Debug(logStr);
            }
        }

        public static bool ExecuteTrans(this Func<bool> func)
        {
            using (var trans = new TransactionScope(TransactionScopeOption.Required, TransactionOption))
            {
                var sw = Stopwatch.StartNew();
                try
                {
                    if (func())
                    {
                        trans.Complete();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
                finally
                {
                    var logStr = "{0}(func) {1}".Formats(func.Method.Name, sw.Elapsed.ToUseSimpleTime());
                    if (sw.ElapsedMilliseconds > WornigMilliseconds)
                    {
                        Log.Warn(logStr);
                    }
                    Log.Debug(logStr);
                }
            }
            return false;
        }
    }
}
