﻿namespace VolvoCash.CrossCutting.Localization
{
    public class LocalizationKeys
    {
        public enum Infraestructure
        {
            info_CannotAddNullEntity,
            info_CannotModifyNullEntity,
            info_CannotRemoveNullEntity,
            info_CannotTrackNullEntity,
            exception_NotMapFoundForTypeAdapter,
            exception_RegisterTypeMapConfigurationElementInvalidTypeValue,
            exception_RegisterTypesMapConfigurationInvalidType,
            exception_InvalidEnumeratedType,
            exception_ContactAlreadyExistsForOtherClient
        };

        public enum DistributedServices
        {
            info_OnExecuted,
            info_OnExecuting,
            info_Parameter,
            messages_RequestCodeMessage,
        }

        public enum Application
        {
            validation_No_Records_Found_Error,
            validation_Null_Parameters_Error,
            validation_Exception,
            messages_CreateTransferDisplayName,
            messages_CreateChargeDisplayName,
            messages_NewCashierEmailBody,
            messages_NewCashierEmailSubject,
            messages_NewContactMessage,
            messages_ResetCashierPasswordEmailSubject,
            messages_ResetCashierPasswordEmailBody,
            messages_ResetAdminPasswordEmailSubject,
            messages_ResetAdminPasswordEmailBody,
            exception_SmsCodeEmpty,
            exception_PhoneNotFound,
            exception_CardNotFound,
            exception_ContactNotFound,
            exception_CannotCreateContactForNonExistingClient,
            exception_ContactAlreadyExistsForOtherClient,
            exception_ContactAlreadyExists,
            exception_ChargeNotFound,
            exception_InvalidCardTypeCode,
            exception_BatchFileIsNull,
            exception_CannotUpdateContactWithEmptyInformation,
            exception_CannotUpdateNonExistingContact,
            exception_InvalidDealerForCashier,
            exception_CashierAlreadyExistsEmail,
            exception_CashierAlreadyExists,
            exception_CashierNotFound,
            exception_AdminAlreadyExists,
            exception_NullContactIdToTransferFounds,
            exception_NullContactToTransferFounds,
            messages_NewAdminEmailSubject,
            messages_NewAdminEmailBody,
            exception_PasswordAndConfirmNotMatch,
            exception_InvalidRechargeTypeCode,
            exception_InvalidBusinessAreaCode,
            exception_InvalidCurrencyCode,
            exception_AdminNotFound,
            exception_BatchAlreadyExists
        }

        public enum Domain
        {
            validation_PropertyIsEmptyOrNull,
            validation_PropertyOutOfRange,
            messages_RechargeMessageDisplayName,
            messages_RechargeMessageDescription,
            messages_TransferFromMessageDisplayName,
            messages_TransferFromMessageDescription,
            messages_TransferToMessageDisplayName,
            messages_TransferToMessageDescription,
            messages_CreationCardMessageDisplayName,
            messages_CreationCardMessageDescription,
            messages_SuccessChargeHeadingToCashier,
            messages_AcceptedChargeHeadingToCashier,
            messages_AcceptedChargeContentToCashier,
            messages_RejectedChargeHeadingToCashier,
            messages_RejectedChargeContentToCashier,
            messages_CanceledChargeHeadingToCashier,
            messages_CanceledChargeContentToCashier,
            messages_OnCreatingChargeHeadingToContact,
            messages_OnCreatingChargeContentToContact,
            exception_PerformRechargeCardIsNull,
            exception_PerformTransferCardIsNull,
            exception_PerformChargeCardIsNull,
            exception_NoEnoughMoneyToWithdraw,
            exception_NoEnoughMoneyToTransfer,
            exception_NoEnoughMoneyToCharge,
            exception_InvalidTransferToSameCard,
            exception_InvalidTransferDifferentContactClient,
            exception_InvalidStatusForCharge,
            exception_PerformTransferOriginCardIsNull,
            exception_PerformTransferDestinyCardIsNull
        }
    }
}
