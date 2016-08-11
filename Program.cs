using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concepts
{
    class Program
    {
        static void Main(string[] args)
        {
            IService svcBase = new PersonService();
            PersonVM vm = new PersonVM(svcBase);
            svcBase.TriggerCompletedEvent();
            Console.ReadKey();
        }
    }

    public delegate void ServiceCompletedEventHandler(object sender, ServiceEventArgs svcEArgs);

    public interface IService
    {
        ServiceCompletedEventHandler serviceEventHandler { get; set; }
        void TriggerCompletedEvent();
    }

    public abstract class ServiceBase : IService
    {
        public IPerson Person { get; set; }
        public ServiceBase()
        {

        }
        public void TriggerCompletedEvent()
        {
            serviceEventHandler.Invoke(this, new ServiceEventArgs(Person));
        }
        public ServiceCompletedEventHandler serviceEventHandler
        {
            get;
            set;
        }
    }

    public class PersonService : ServiceBase
    {
        public PersonService()
        {
            Person = new Person();
        }
    }

    public class PersonVM
    {
        public PersonVM(IService svcPerson)
        {
            svcPerson.serviceEventHandler += Handler;
        }

        private void Handler(object sender, ServiceEventArgs svcEArgs)
        {
            Console.WriteLine("Triggered");
        }
    }

    public class ServiceEventArgs : EventArgs
    {
        public IPerson person = null;
        public ServiceEventArgs(IPerson person)
        {
            this.person = person;
        }
    }

    public interface IPerson
    {
        string Name { get; set; }
    }

    public class Person : IPerson
    {
        public string Name
        {
            get;
            set;
        }
    }
}
