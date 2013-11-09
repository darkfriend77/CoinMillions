// <copyright file="MethodName.cs" company="CoinMillions">
// Copyright (c) 2013 CoinMillions. All rights reserved.
// </copyright>
// <author>superreeen</author>
// <date>08.11.2013</date>
// <summary>Implements the method name class</summary>
namespace CoinMillions.BitcoinClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> Values that represent MethodName. </summary>
    /// <remarks> superreeen, 08.11.2013. </remarks>
    public enum MethodName
    {
        /// <summary> An enum constant representing the add multi signal address option. </summary>
        AddMultiSigAddress, // <nrequired> <'["key","key"]'> [account]
        /// <summary> An enum constant representing the create multi signal option. </summary>
        CreateMultiSig, // <nrequired> <'["key","key"]'>
        /// <summary> An enum constant representing the create raw transaction option. </summary>
        CreateRawTransaction, //[{"txid":txid,"vout":n},...] {address:amount,...}
        /// <summary> An enum constant representing the import priv key option. </summary>
        ImportPrivKey, // <bitcoinprivkey> [label] [rescan=true]
        /// <summary> An enum constant representing the list unspent option. </summary>
        ListUnspent, //[minconf=1] [maxconf=9999999] ["address",...]
        /// <summary> An enum constant representing the lock unspent option. </summary>
        LockUnspent, // unlock? [array-of-Objects]
        /// <summary> An enum constant representing the send many option. </summary>
        SendMany, // <fromaccount> {address:amount,...} [minconf=1] [comment]
        /// <summary> An enum constant representing the set account option. </summary>
        SetAccount, // <bitcoinaddress> <account>
        /// <summary> An enum constant representing the sign raw transaction option. </summary>
        SignRawTransaction, //<hex string> [{"txid":txid,"vout":n,"scriptPubKey":hex,"redeemScript":hex},...] [<privatekey1>,...] [sighashtype="ALL"]
        /// <summary> An enum constant representing the get balance option. </summary>
        GetBalance,
        /// <summary> An enum constant representing the get block option. </summary>
        GetBlock,
        /// <summary> An enum constant representing the get raw transaction option. </summary>
        GetRawTransaction,
        /// <summary> An enum constant representing the decode raw transaction option. </summary>
        DecodeRawTransaction,
        /// <summary> An enum constant representing the get account option. </summary>
        GetAccount,
        /// <summary> An enum constant representing the get block count option. </summary>
        GetBlockCount,
        /// <summary> An enum constant representing the get block hash option. </summary>
        GetBlockHash,
        /// <summary> An enum constant representing the get difficulty option. </summary>
        GetDifficulty,
        /// <summary> An enum constant representing the get generate option. </summary>
        GetGenerate,
        /// <summary> An enum constant representing the get hashes per security option. </summary>
        GetHashesPerSec,
        /// <summary> An enum constant representing the get information option. </summary>
        GetInfo,
        /// <summary> An enum constant representing the get peer information option. </summary>
        GetPeerInfo,
        /// <summary> An enum constant representing the get raw memory pool option. </summary>
        GetRawMemPool,
        /// <summary> An enum constant representing the get mining information option. </summary>
        GetMiningInfo,
        /// <summary> An enum constant representing the backup wallet option. </summary>
        BackupWallet,
        /// <summary> An enum constant representing the dump priv key option. </summary>
        DumpPrivKey,
        /// <summary> An enum constant representing the encrypt wallet option. </summary>
        EncryptWallet,
        /// <summary> An enum constant representing the get account address option. </summary>
        GetAccountAddress,
        /// <summary> An enum constant representing the get addresses by account option. </summary>
        GetAddressesByAccount,
        /// <summary> An enum constant representing the get block template option. </summary>
        GetBlockTemplate,
        /// <summary> An enum constant representing the get connection count option. </summary>
        GetConnectionCount,
        /// <summary> An enum constant representing the get new address option. </summary>
        GetNewAddress,
        /// <summary> An enum constant representing the get received by account option. </summary>
        GetReceivedByAccount,
        /// <summary> An enum constant representing the get received by address option. </summary>
        GetReceivedByAddress,
        /// <summary> An enum constant representing the get transaction option. </summary>
        GetTransaction,
        /// <summary> An enum constant representing the get work option. </summary>
        GetWork,
        /// <summary> An enum constant representing the help option. </summary>
        Help,
        /// <summary> An enum constant representing the list accounts option. </summary>
        ListAccounts,
        /// <summary> An enum constant representing the list address groupings option. </summary>
        ListAddressGroupings,
        /// <summary> An enum constant representing the list received by account option. </summary>
        ListReceivedByAccount,
        /// <summary> An enum constant representing the list received by address option. </summary>
        ListReceivedByAddress,
        /// <summary> An enum constant representing the list since block option. </summary>
        ListSinceBlock,
        /// <summary> An enum constant representing the list transactions option. </summary>
        ListTransactions,
        /// <summary> An enum constant representing the list lock unspent option. </summary>
        ListLockUnspent,
        /// <summary> An enum constant representing the set transmit fee option. </summary>
        SetTxFee,
        /// <summary> An enum constant representing the wallet lock option. </summary>
        WalletLock,
        /// <summary> An enum constant representing the validate address option. </summary>
        ValidateAddress,
        /// <summary> An enum constant representing the stop option. </summary>
        Stop,
        /// <summary> An enum constant representing the set generate option. </summary>
        SetGenerate,
        /// <summary> An enum constant representing the send to address option. </summary>
        SendToAddress,
        /// <summary> An enum constant representing the send raw transaction option. </summary>
        SendRawTransaction,
        /// <summary> An enum constant representing the add node option. </summary>
        AddNode,
        /// <summary> An enum constant representing the get added node information option. </summary>
        GetAddedNodeInfo,
        /// <summary> An enum constant representing the get transmit out option. </summary>
        GetTxOut,
        /// <summary> An enum constant representing the get transmit out set information option. </summary>
        GetTxOutSetInfo,
        /// <summary> An enum constant representing the key pool refill option. </summary>
        KeyPoolRefill,
        /// <summary> An enum constant representing the send from option. </summary>
        SendFrom,
        /// <summary> An enum constant representing the sign message option. </summary>
        SignMessage,
        /// <summary> An enum constant representing the submit block option. </summary>
        SubmitBlock,
        /// <summary> An enum constant representing the verify message option. </summary>
        VerifyMessage,
        /// <summary> An enum constant representing the wallet passphrase option. </summary>
        WalletPassphrase,
        /// <summary> An enum constant representing the wallet passphrase change option. </summary>
        WalletPassphraseChange,
        /// <summary> An enum constant representing the move option. </summary>
        Move
    }
}
