using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp1
{
    class WiFiModule
    {
        // NOTE: Commands obtained from Wifi Module datasheet/command list
        // \\abfsi\Engineering\R & D\WiFi Modules\*\*DataSheet.PDF
        // Constants
        private string COMMAND_SUFFIX_STRING { get; set; }
        private string COMMAND_SET_OPERATION_MODE { get; set; }
        private string COMMAND_SOFT_RESET { get; set; }
        private string COMMAND_SEND_DATA { get; set; }
        private string COMMAND_HANDLE_URL_REQUEST { get; set; }
        private string COMMAND_HTTP_POST { get; set; }

        // Module response strings
        private string OK_RESPONSE_STRING { get; set; }
        private string ERROR_RESPONSE_STRING { get; set; }
        private string LTCP_CONNECTION_STRING { get; set; }
        private string TCP_CONNECTION_READ_STRING { get; set; }  // Client to server request (TCP connection)
        private string URL_REQUEST_STRING { get; set; }          // Client to server reqeust (urlreq)
        private string HTTP_RSP_RESPONSE_STRING { get; set; }  // Server ti client response (after an http post/get etc...)

        

        // <summary>
        //  Constructor
        // </summary>
        public WiFiModule()
        {

        }

        public string getSendDataCommand(int socketDescriptor, int bufferLength, string destIpAddr, int destPort, string dataBuffer)
        {
            String command = String.Format(COMMAND_SEND_DATA, socketDescriptor, bufferLength, destIpAddr, destPort, dataBuffer);
            command += COMMAND_SUFFIX_STRING;
            return command;
        }

        // <summary>
        // This method will retun the command to send the content of a webpage.
        // Note: Can be found in the RS9113-WiseConnect-Software-PRM-v1.5.0.pdf, page 246 Section 8.59 Web Page Bypass.
        // </summary>
        // <param name="contentLength"></param>
        // <param name="moreChunks"></param>
        // <param name="content"></param>
        // <returns></returns>
        public string getHandleUrlResponseCommand(int contentLength, int moreChunks, String content)
        {
            String command = String.Format(COMMAND_HANDLE_URL_REQUEST, contentLength, moreChunks, content);
            command += COMMAND_SUFFIX_STRING;
            return command;
        }

        // <summary>
        // This method builds an http post request.
        // </summary>
        // <param name="httpsEnable"></param>
        // <param name="httpPort"></param>
        // <param name="username"></param>
        // <param name="password"></param>
        // <param name="hostname"></param>
        // <param name="ipAddr"></param>
        // <param name="url"></param>
        // <param name="extendedHeader"></param>
        // <param name="data"></param>
        // <returns></returns>
        public byte[] getHttpPostCommand(int httpsEnable, int httpPort, String username, String password, String hostname, String ipAddr, String url, String extendedHeader, String data)
        {
            String commandString = String.Format(COMMAND_HTTP_POST, httpsEnable, httpPort, username, password, hostname, ipAddr, url, extendedHeader, data);
            commandString += COMMAND_SUFFIX_STRING;

            byte[] commandBytes = Encoding.ASCII.GetBytes(commandString);

            for (int index = 0; index < (commandBytes.Length - 2); index++)
            {
                // Find the \r\n ( -2 = ignoring the command suffix) in the command string (from extendedHeader), replace with 0xDB 0xDC
                if (commandBytes[index] == 0x0D)
                {
                    commandBytes[index] = 0xDB;
                }
                else if (commandBytes[index] == 0x0A)
                {
                    commandBytes[index] = 0xDC;
                }
            }

            return commandBytes;
        }

        // <summary>
        // 
        // </summary>
        // <param name="operMode"></param>
        // <param name="feature"></param>
        // <param name="tcpIpFeature"></param>
        // <param name="customFeature"></param>
        // <returns></returns>
        public String getOperationModeString(Int64 operMode, Int64 feature, Int64 tcpIpFeature, Int64 customFeature)
        {
            String command = String.Format(COMMAND_SET_OPERATION_MODE, operMode, feature, tcpIpFeature, customFeature);
            command += COMMAND_SUFFIX_STRING;
            return command;
        }

        // <summary>
        // THis method gets the command list to configure the WiFi module
        // </summary>
        // <returns></returns>
        public ArrayList getConfigurationCommandList()
        {
            ArrayList commandList = new ArrayList();

            // Ensure that auto-rejoin is disabled
            commandList.Add("at+rsi_cfgenable=0\r\n");
            // Reset the module
            commandList.Add("at+rsi_reset\r\n");
            // Set operation mode
            commandList.Add("at+rsi_opermode=0,1,452,0\r\n");
            // Set band
            commandList.Add("at+rsi_band=0\r\n");
            // Init wifi module
            commandList.Add("at+rsi_init\r\n");
            // AP scan
            commandList.Add("at+rsi_scan=0\r\n");
            // Set WiFi password
            commandList.Add("at+rsi_psk=1,l3tm30ut\r\n");
            // Join WiFi network
            commandList.Add("at+rsi_join=VitecGuest,0,2,2\r\n");
            // Configure IP Address
            commandList.Add("at+rsi_ipconf=1,0,0,0\r\n");
            // Set auto re-join AP parameters
            commandList.Add("at+rsi_rejoin_params=3,5,5,1\r\n");
            // Enable auto-join to AP
            commandList.Add("at+rsi_cfgenable=1\r\n");
            // Save config settings
            commandList.Add("at+rsi_cfgsave\r\n");

            return commandList;
        }


    }
}