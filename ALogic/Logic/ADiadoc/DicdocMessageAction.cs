using ALogic.Logic.Old;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Invoicing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALogic.Logic.ADiadoc
{
    public static class DicdocMessageAction
    {

        internal static void ReadDocSign(Message message, System.Data.DataRow row)
        {
            try
            {
                foreach (var ent in message.Entities)
                {
                    if (ent.AttachmentType == AttachmentType.UniversalTransferDocumentBuyerTitle)
                    {
                        DBDiadoc.UpdateDocStatus(row["idDoc"], 6);
                        DBDiadoc.UpdateDiadocMessage(row["idMessage"].ToString(), 200);
                        var doc = DBDiadoc.GetDocHeader(row["idDoc"]);
                        Logger.SendMessage("Документ подтвержден клиентом. IdDoc = " + row["idDoc"].ToString() + " №" + doc["NomDoc"].ToString() + " от " + doc["DateDoc"].ToString());
                        break;
                    }

                    if (ent.AttachmentType == AttachmentType.XmlSignatureRejection)
                    {
                        DBDiadoc.UpdateDocStatus(row["idDoc"], 5);
                        DBDiadoc.UpdateDiadocMessage(row["idMessage"].ToString(), 300);
                        var doc = DBDiadoc.GetDocHeader(row["idDoc"]);
                        Logger.SendMessage("Получен отказ клиента! IdDoc = " + row["idDoc"].ToString() + " №" + doc["NomDoc"].ToString() + " от " + doc["DateDoc"].ToString());
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorMessage("Ошибка в ReadDocSign: " + ex.Message);
            }
        }

        internal static void SeeDoc(Message invoiceMessage, System.Data.DataRow row, Sertificate c)
        {
            var ent = invoiceMessage.Entities.FirstOrDefault(p => p.AttachmentType == AttachmentType.InvoiceConfirmation);
            if (ent == null) return;
            var parentId = ent.EntityId;

            var receipt = c.Api.GenerateInvoiceDocumentReceiptXml(c.AuthTokenCert, c.BoxId, invoiceMessage.MessageId, parentId, DocCreator.GetSigner(c.Inn));

            var receiptAttachment = new ReceiptAttachment();
            receiptAttachment.ParentEntityId = parentId;
            receiptAttachment.SignedContent = new SignedContent();
            receiptAttachment.SignedContent.Content = receipt.Content;
            receiptAttachment.SignedContent.Signature = c.Crypt.Sign(receipt.Content, c.FileContent);

            var mp = new MessagePatchToPost();
            mp.BoxId = c.BoxId;
            mp.MessageId = invoiceMessage.MessageId;
            mp.Receipts.Add(receiptAttachment);

            c.Api.PostMessagePatch(c.AuthTokenCert, mp);
        }


        internal static void SignDoc(Message message, System.Data.DataRow row, Sertificate c)
        {
            var parentId = message.Entities.First(p => p.AttachmentType == AttachmentType.UniversalCorrectionDocumentBuyerTitle).EntityId;
            var signer = DocCreator.GetSigner(c.Inn);
            var content = new AcceptanceCertificateBuyerTitleInfo();
            content.Signature = new AcceptanceCertificateSignatureInfo();
            content.Signature.SignatureDate = DateTime.Now.ToShortDateString();
            content.Signature.Official = new Official();
            content.Signature.Official.Surname = signer.SignerDetails.Surname;
            content.Signature.Official.FirstName = signer.SignerDetails.FirstName;
            content.AdditionalInfo = "подписан из системы";
            content.Signer = signer;

            var buyerTitle = c.Api.GenerateAcceptanceCertificateXmlForBuyer(c.AuthTokenCert, content, c.BoxId, message.MessageId, parentId);

            var receiptAttachment = new ReceiptAttachment();
            receiptAttachment.ParentEntityId = parentId;
            receiptAttachment.SignedContent = new SignedContent();
            receiptAttachment.SignedContent.Content = buyerTitle.Content;
            receiptAttachment.SignedContent.Signature = c.Crypt.Sign(buyerTitle.Content, c.FileContent);

            var mp = new MessagePatchToPost();
            mp.BoxId = c.BoxId;
            mp.MessageId = message.MessageId;
            mp.AddXmlAcceptanceCertificateBuyerTitle(receiptAttachment);

            c.Api.PostMessagePatch(c.AuthTokenCert, mp);
        }

        //RevocationRequest  предложение об анулировании

        internal static void UnSignDoc(Message message, System.Data.DataRow row)
        {
            //
        }

        internal static object RealAnulateDocSign(Message message, System.Data.DataRow row)
        {
            throw new NotImplementedException();
        }

        internal static object AnulateDoc(Message message, System.Data.DataRow row)
        {
            throw new NotImplementedException();
        }

        internal static void SignAnulate(Message message, System.Data.DataRow row)
        {
            throw new NotImplementedException();
        }

        internal static void UnSignAnulate(Message message, System.Data.DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
