using System;
using Thirdparty.Wechat.Service.WxJsonMessages;
using Thirdparty.Wechat.Service.WxModels;

namespace Thirdparty.Wechat.Service
{
    /// <summary>
    /// 一个工厂
    /// </summary>
    internal class WxJsonMessageFactory
    {
        public static WxJsonMessage GetJsonMessage(IWxOpenMessage message)
        {
            WxJsonMessage jsonMessage = null;
            switch (message.MessageType)
            {
                case WxMessageTypes.Mpnews:
                    jsonMessage = new WxJsonOpenImageTextMessage(message as WxOpenImageTextMessage);
                    break;
                case WxMessageTypes.Text:
                    jsonMessage = new WxJsonOpenTextMessage(message as WxOpenTextMessage);
                    break;
                case WxMessageTypes.Voice:
                    break;
                case WxMessageTypes.Music:
                    break;
                case WxMessageTypes.Image:
                    break;
                case WxMessageTypes.Video:
                    break;
                case WxMessageTypes.Wxcard:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(message.MessageType), message.MessageType, null);
            }

            return jsonMessage;
        }

        public static WxJsonMessage GetJsonMessage(IWxTagMessage message)
        {
            WxJsonMessage jsonMessage = null;
            switch (message.MessageType)
            {
                case WxMessageTypes.Mpnews:
                    jsonMessage = new WxJsonTagImageTextMessage(message as WxTagImageTextMessage);
                    break;
                case WxMessageTypes.Text:
                    jsonMessage = new WxJsonTagTextMessage(message as WxTagTextMessage);
                    break;
                case WxMessageTypes.Voice:
                    break;
                case WxMessageTypes.Music:
                    break;
                case WxMessageTypes.Image:
                    break;
                case WxMessageTypes.Video:
                    break;
                case WxMessageTypes.Wxcard:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(message.MessageType), message.MessageType, null);
            }

            return jsonMessage;
        }

        public static WxJsonMessage GetJsonMessage(IWxCustomMessage message)
        {
            WxJsonMessage jsonMessage = null;
            switch (message.MessageType)
            {
                case WxMessageTypes.Mpnews:
                    jsonMessage = new WxJsonCustomImageTextMessage(message as WxCustomImageTextMessage);
                    break;
                case WxMessageTypes.Text:
                    jsonMessage = new WxJsonCustomTextMessage(message as WxCustomTextMessage);
                    break;
                case WxMessageTypes.Voice:
                    break;
                case WxMessageTypes.Music:
                    break;
                case WxMessageTypes.Image:
                    break;
                case WxMessageTypes.Video:
                    break;
                case WxMessageTypes.Wxcard:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(message.MessageType), message.MessageType, null);
            }

            return jsonMessage;
        }
    }
}