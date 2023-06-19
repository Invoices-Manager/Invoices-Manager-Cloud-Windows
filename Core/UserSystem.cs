﻿using Accessibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InvoicesManager.Core
{
    public class UserSystem
    {
        private MainWindow _mw;
        public UserSystem(MainWindow mw)
            => _mw = mw;

        public bool Login(string username, string password)
        {
            //check if the data is vaild
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
                return false;

            //GetBearerToken
            EnvironmentsVariable.BearerToken = GetBearerToken(username, password);

            //if the login was not sucessful, then return a false
            if (String.IsNullOrEmpty(EnvironmentsVariable.BearerToken)) 
                return false;

            //if the login was sucessful, then enable the main window buttons
            _mw.UI_Login();
            return true;
        }

        public bool Logout()
        {
            if (!LogoutFromApi())
                return false;

            EnvironmentsVariable.BearerToken = String.Empty;
           _mw.UI_Logout();

            return true;
        }

        public bool Create(string username, string password)
        {
            //check if the data is vaild
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
                return false;


            return true;
        }

        private bool LogoutFromApi()
        {
            WebRequestSystem _wr = new WebRequestSystem(EnvironmentsVariable.API_ENDPOINT_USER_LOGOUT);

            _wr.SetRequestMethod("DELETE");
            _wr.SetHeaders(new WebHeaderCollection()
            {
                { "bearerToken", EnvironmentsVariable.BearerToken },
                { "Content-Type", "application/json" }
            });

            _wr.SendRequest();

            HttpStatusCode statusCode = _wr.GetStatusCode();
            string responseBody = _wr.GetResponseBody();
            bool isSuccess = _wr.IsSuccess();

            if (!isSuccess)
            {
                throw new Exception("Error: " + statusCode + " " + responseBody);
            }

            return isSuccess;
        }

        private string GetBearerToken(string username, string password)
        {
            WebRequestSystem _wr = new WebRequestSystem(EnvironmentsVariable.API_ENDPOINT_USER_LOGIN);

            _wr.SetRequestMethod("PUT");
            _wr.SetHeaders(new WebHeaderCollection()
            {
                { "Content-Type", "application/json" }
            });
            _wr.SetBody(JsonConvert.SerializeObject(new { username, password }));
            _wr.SendRequest();

            HttpStatusCode statusCode = _wr.GetStatusCode();
            string responseBody = _wr.GetResponseBody();
            bool isSuccess = _wr.IsSuccess();

            if (!isSuccess)
            {
                throw new Exception("Error: " + statusCode + " " + responseBody);
            }

            WebResponseModel response = JsonConvert.DeserializeObject<WebResponseModel>(responseBody);

            return JsonConvert.SerializeObject(response.Args["token"].ToString()).Trim('"');
        }
    }
}
