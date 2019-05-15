using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

using System.Diagnostics;

using System.Linq;
using System.Threading.Tasks;

namespace ServerNetCore.Models
{
    // Дубль кода из проекта ServerAPI чтобы устранить зависимости от .NET framework проекта server api
    //TODO to refactor
    public class Error
    {
        public Error()
        { }

        public Error(int code, string msg)
        {
            Code = code;
            Message = msg;
        }

        public int Code;
        public string Message;

        public override string ToString()
        {
            return $"code {Code} : {Message}";
        }
    }

    public interface IResponse
    {
        bool BoolResult { get; set; }
        Error Error { get; set; }
    }

    public class ResultRequest<T> : ResultRequest
    {
        public T Data;
    }

    public interface IJob
    {
        Guid? Id { get; set; }
        List<Param> Parameters { get; set; }
        string Method { get; set; }

        string Sender { get; set; }
        string Reciever { get; set; }
    }

    public class Result : IJob, IResponse
    {
        public bool BoolResult { get; set; }
        public int? IntResult { get; set; }
        public string StrResult { get; set; }

        public Error Error { get; set; }

        public Guid? Id { get; set; }
        public List<Param> Parameters { get; set; }
        public string Method { get; set; }

        public string VirtualDriverId { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public double? DblResult { get; set; }
    }

    [DebuggerDisplay("{T}\t {Name}\t {Data}")]
    public struct Param
    {
        public Param(string name, string data, string type)
        {
            Name = name;
            Data = data;
            T = type;
        }

        public Param(string name, object data)
        {
            if (data == null) throw new ArgumentNullException("не корректные данные на входе", nameof(data));

            Name = name;
            Data = data.ToString();
            switch (data)
            {
                case int _: T = "int"; break;
                case string _: T = "string"; break;
                case double _: T = "double"; break;
                case bool _: T = "bool"; break;
                default: throw new NotImplementedException("Не реализована вся логика");
            }
        }

        public string Name;

        public string Data;

        /// <summary>
        /// Тип аргумента
        /// </summary>
        public string T;
    }

    public class Job : IJob
    {
        /// <summary>
        /// ИД задания на сервере
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Данные задания поступившие от 1С в исходном виде
        /// </summary>
        public List<Param> Parameters { get; set; }

        public string Method { get; set; }

        public string DeviceId { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
    }

    public class ResultRequest : IResponse
    {
        public bool BoolResult { get; set; }
        public Error Error { get; set; }

        public static ResultRequest Ok()
        {
            return new ResultRequest() { BoolResult = true };
        }

        //public static Response<string> SetError(Error error, string addInfo = null)
        //{
        //    return new Response<string>
        //    {
        //        BoolResult = false,
        //        Error = error,
        //        Data = addInfo
        //    };
        //}
    }
}