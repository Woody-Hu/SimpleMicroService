using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using UnityUtility;

namespace SimpleMicroService.Models
{
    [Compent(RegistByClass = true,Singleton = true)]
    public class ServiceInfo
    {
        /// <summary>
        /// 服务字典
        /// </summary>
        private Dictionary<string, List<ClinentInfo>> m_dicServiceClientes = new Dictionary<string, List<ClinentInfo>>();

        /// <summary>
        /// 使用的读写锁
        /// </summary>
        private ReaderWriterLockSlim m_useRWLock = new ReaderWriterLockSlim();

        public ServiceInfo()
        {
        }

        /// <summary>
        /// 添加一个服务
        /// </summary>
        /// <param name="inputCilentInfo"></param>
        public void AddService(ClinentInfo inputCilentInfo)
        {
            try
            {
                m_useRWLock.EnterWriteLock();

                if (!m_dicServiceClientes.ContainsKey(inputCilentInfo.ServiceName))
                {
                    m_dicServiceClientes.Add(inputCilentInfo.ServiceName, new List<Models.ClinentInfo>());
                }

                m_dicServiceClientes[inputCilentInfo.ServiceName].Add(inputCilentInfo);
            }
            catch (Exception)
            {
                ;
            }
            finally
            {
                m_useRWLock.ExitWriteLock();
            }
  
        }

        /// <summary>
        /// 是否包含服务
        /// </summary>
        /// <param name="inputServiceName"></param>
        /// <returns></returns>
        public bool IfContainsService(string inputServiceName)
        {
            try
            {
                m_useRWLock.EnterWriteLock();

                if (!m_dicServiceClientes.ContainsKey(inputServiceName))
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                m_useRWLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 根据服务名获取服务客户端列表
        /// </summary>
        /// <param name="inputServiceName"></param>
        /// <returns></returns>
        public List<ClinentInfo> GetServices(string inputServiceName)
        {
            try
            {
                m_useRWLock.EnterReadLock();
                if (!m_dicServiceClientes.ContainsKey(inputServiceName))
                {
                    return null;
                }
                //返回副本防止并发异常
                else
                {
                    List<ClinentInfo> returnValues = new List<Models.ClinentInfo>();
                    returnValues.AddRange(m_dicServiceClientes[inputServiceName]);
                    return returnValues;
                }

            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                m_useRWLock.ExitReadLock();
            }
        }

        public override string ToString()
        {
            try
            {
                m_useRWLock.EnterReadLock();

                StringBuilder tempStringBuilder = new StringBuilder();
                tempStringBuilder.AppendLine("Now Applications:");

                foreach (var oneKVP in m_dicServiceClientes)
                {
                    tempStringBuilder.AppendLine(string.Format("ApplicationName:{0}   Count:{1}", oneKVP.Key, oneKVP.Value.Count));
                }

                return tempStringBuilder.ToString();
                
            }
            finally
            {
                m_useRWLock.ExitReadLock();
            }
            
        }
    }
}