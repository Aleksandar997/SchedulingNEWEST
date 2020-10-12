using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Base
{
    public class ResponseBase<T> 
    {
        public List<ResponseMessage> Messages { get; set; }
        public T Data { get; set; }
        public ResponseStatus Status { get; set; }
        public int Count { get; set; }

        public ResponseBase(T data, List<ResponseMessage> messages, int count = 0)
        {
            Data = data;
            Status = messages.Any() ? ResponseStatus.Error : ResponseStatus.Success;
            Messages = messages;
            Count = count;
        }
        public ResponseBase(T data, int count = 0)
        {
            Data = data;
            Status = ResponseStatus.Success;
            Count = count;
        }
        public ResponseBase() { }
        public static ResponseBase<T> ReturnResponse(T data, ResponseStatus status, List<ResponseMessage> messages = null)
        {
            return new ResponseBase<T>()
            {
                Messages = messages,
                Data = data,
                Status = status
            };
        }

        public ResponseBase<T> Bind(Func<T,T> func)
        {
            Data = func(Data);
            return this;
        }


        public static ResponseBase<T> Error(List<ResponseMessage> messages)
        {
            return new ResponseBase<T>()
            {
                Messages = messages,
                Status = ResponseStatus.Error
            };
        }

        public static ResponseBase<T> Error(string message)
        {
            return new ResponseBase<T>()
            {
                Messages = new List<ResponseMessage>() { new ResponseMessage(message) },
                Status = ResponseStatus.Error
            };
        }

        public static ResponseBase<T> Success(T data, List<ResponseMessage> messages)
        {
            return new ResponseBase<T>()
            {
                Data = data,
                Messages = messages,
                Status = ResponseStatus.Success
            };
        }

        public static ResponseBase<T> Success(T data, List<ResponseMessage> messages, int count)
        {
            return new ResponseBase<T>()
            {
                Data = data,
                Messages = messages,
                Count = count,
                Status = ResponseStatus.Success
            };
        }
    }
    public class ResponseMessage
    {
        public string Value { get; set; }
        public string Code { get; set; }

        public ResponseMessage(string code = null)
        {
            Code = code;
        }
    }
    public enum ResponseStatus
    {
        Success, Error, PasswordExpired
    }

    public static class ResponseBaseExtensions
    {
        public static ResponseBase<Tres> Then<T, Tres>(this ResponseBase<T> result, Func<T, Tres> func)
        {
            return new ResponseBase<Tres>()
            {
                Count = result.Count,
                Messages = result.Messages,
                Status = result.Status,
                Data = func(result.Data)
            };
        }

        public static async Task<ResponseBase<Tres>> Then<T, Tres>(this Task<ResponseBase<T>> res, Func<T, Tres> func)
        {
            var result = await res;
            return result.Then(func);
        }
    }
}

