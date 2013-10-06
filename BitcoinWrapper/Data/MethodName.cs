using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinWrapper.Data
{



    public enum MethodName
    {
        //addmultisigaddress <nrequired> <'["key","key"]'> [account]
        //createmultisig <nrequired> <'["key","key"]'>
        createrawtransaction, //[{"txid":txid,"vout":n},...] {address:amount,...}
        //importprivkey <bitcoinprivkey> [label] [rescan=true]
        //listunspent [minconf=1] [maxconf=9999999] ["address",...]
        //lockunspent unlock? [array-of-Objects]
        //sendmany <fromaccount> {address:amount,...} [minconf=1] [comment]
        //setaccount <bitcoinaddress> <account>
        signrawtransaction, //<hex string> [{"txid":txid,"vout":n,"scriptPubKey":hex,"redeemScript":hex},...] [<privatekey1>,...] [sighashtype="ALL"]
        //verifymessage <bitcoinaddress> <signature> <message>

        getbalance,
        getblock,
        getrawtransaction,
        decoderawtransaction,
        getaccount,
        getblockcount,
        getblockhash,
        getdifficulty,
        getgenerate,
        gethashespersec,
        getinfo,
        getpeerinfo,
        getrawmempool,
        getmininginfo,
        backupwallet,
        dumpprivkey,
        encryptwallet,
        getaccountaddress,
        getaddressesbyaccount,
        getblocktemplate,
        getconnectioncount,
        getnewaddress,
        getreceivedbyaccount,
        getreceivedbyaddress,
        gettransaction,
        getwork,
        help,
        listaccounts,
        listaddressgroupings,
        listreceivedbyaccount,
        listreceivedbyaddress,
        listsinceblock,
        listtransactions,
        listlockunspent,
        settxfee,
        walletlock,
        validateaddress,
        stop,
        setgenerate,
        sendtoaddress,
        sendrawtransaction,
        addnode,
        getaddednodeinfo,
        gettxout,
        gettxoutsetinfo,
        keypoolrefill,
        sendfrom,
        signmessage,
        submitblock,
        verifymessage,
        walletpassphrase,
        walletpassphrasechange,
        move
    }
}
