using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace libc.extended {
    public class FluentResult {
        [JsonIgnore]
        public bool Success => Errors.Count == 0;
        [JsonIgnore]
        public bool Fail => Errors.Count > 0;
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Messages { get; set; } = new List<string>();
        public FluentResult AddError(params string[] errors) {
            if (errors != null)
                Errors.AddRange(errors);
            return this;
        }
        public FluentResult AddMessage(params string[] messages) {
            if (messages != null)
                Messages.AddRange(messages);
            return this;
        }
        /// <summary>
        /// </summary>
        /// <param name="delimiter">null means <see cref="Environment.NewLine" /></param>
        /// <returns></returns>
        public string ConcatErrors(string delimiter = null) {
            var d = delimiter ?? Environment.NewLine;
            return Errors == null ? string.Empty : string.Join(d, Errors);
        }
        /// <summary>
        /// </summary>
        /// <param name="delimiter">null means <see cref="Environment.NewLine" /></param>
        /// <returns></returns>
        public string ConcatMessages(string delimiter = null) {
            var d = delimiter ?? Environment.NewLine;
            return Messages == null ? string.Empty : string.Join(d, Messages);
        }
    }
    public class FluentResult<T> : FluentResult {
        public T Value { get; set; }
        public FluentResult<T> SetValue(T value) {
            Value = value;
            return this;
        }
    }
}