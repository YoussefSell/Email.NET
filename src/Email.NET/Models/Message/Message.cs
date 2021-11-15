﻿namespace Email.NET
{
    using Factories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;

    /// <summary>
    /// defines the mail message.
    /// </summary>
    public partial class Message
    {
        /// <summary>
        /// Gets or sets the priority of this e-mail message.
        /// </summary>
        public Priority Priority { get; }

        /// <summary>
        /// the subject of the message
        /// </summary>
        public MessageSubject Subject { get; set; }

        /// <summary>
        /// Get message HMTL body.
        /// </summary>
        public MessageBody HtmlBody { get; }
        
        /// <summary>
        /// Get message plain text body.
        /// </summary>
        public MessageBody PlainTextBody { get; }

        /// <summary>
        /// the email address to send From it
        /// </summary>
        public MailAddress From { get; private set; }

        /// <summary>
        /// the email address to send the email to it
        /// </summary>
        public ICollection<MailAddress> To { get; }

        /// <summary>
        /// Gets or sets the ReplyTo address for the mail message.
        /// </summary>
        public ICollection<MailAddress> ReplyTo { get; }

        /// <summary>
        /// Bcc stands for blind carbon copy which is similar to that of Cc except that the Email address
        /// of the recipients specified in this field do not appear in the received message header and the
        /// recipients in the To or Cc fields will not know that a copy sent to these address.
        /// </summary>
        public ICollection<MailAddress> Bcc { get; }

        /// <summary>
        /// Cc: (Carbon Copy) - Put the email address(es) here if you are sending a copy for their information
        /// (and you want everyone to explicitly see this) Bcc: (Blind Carbon Copy) - Put the email address here
        /// if you are sending them a Copy and you do not want the other recipients to see that you sent it to this contact.
        /// </summary>
        public ICollection<MailAddress> Cc { get; }

        /// <summary>
        /// list of attachments
        /// </summary>
        public ICollection<Attachment> Attachments { get; }

        /// <summary>
        /// Gets the e-mail headers that are transmitted with this e-mail message.
        /// </summary>
        public IDictionary<string, string> Headers { get; }
    }

    /// <summary>
    /// partial part for <see cref="Message"/>
    /// </summary>
    public partial class Message
    {
        /// <summary>
        /// create instance of <see cref="Message"/> with all properties
        /// </summary>
        /// <param name="subject">the message subject</param>
        /// <param name="plainTextBody">the message plan text body</param>
        /// <param name="htmlBody">the message HTML body</param>
        /// <param name="from">the from mail address</param>
        /// <param name="to">the recipient email address</param>
        /// <param name="priority">message priority</param>
        /// <param name="replyTo">reply to mail addresses</param>
        /// <param name="bcc">bcc mail addresses</param>
        /// <param name="cc">cc mail addresses</param>
        /// <param name="attachments">attachments list</param>
        /// <param name="headers">headers collection</param>
        public Message(MessageSubject subject, MessageBody plainTextBody, MessageBody htmlBody, MailAddress from, ICollection<MailAddress> to, Priority priority, ICollection<MailAddress> replyTo, ICollection<MailAddress> bcc, ICollection<MailAddress> cc, ICollection<Attachment> attachments, IDictionary<string, string> headers)
        {
            if (to is null)
                throw new ArgumentNullException(nameof(to));

            if (!to.Any())
                throw new ArgumentException("you must specify at least one recipient email in the 'To' list", nameof(to));

            To = to;
            From = from;
            Priority = priority;

            Subject = subject;
            HtmlBody = htmlBody;
            PlainTextBody = plainTextBody;

            Cc = cc ?? new HashSet<MailAddress>();
            Bcc = bcc ?? new HashSet<MailAddress>();
            ReplyTo = replyTo ?? new HashSet<MailAddress>();
            Headers = headers ?? new Dictionary<string, string>();
            Attachments = attachments ?? new HashSet<Attachment>();
        }

        /// <summary>
        /// set the from mail address
        /// </summary>
        /// <param name="from">the from mail address</param>
        internal void SetFrom(MailAddress from)
            => From = from;

        /// <summary>
        /// create an instance of <see cref="MessageComposer"/> to start composing the message data.
        /// </summary>
        /// <returns>instance of <see cref="MessageComposer"/>.</returns>
        public static MessageComposer Compose()
            => new MessageComposer();
    }
}
