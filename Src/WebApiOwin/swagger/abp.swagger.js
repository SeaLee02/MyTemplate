var abp = abp || {};
(function () {

    /* Swagger */

    abp.swagger = abp.swagger || {};

    abp.swagger.addAuthToken = function () {
        var authToken = abp.auth.getToken();
        if (!authToken) {
            return false;
        }

        var cookieAuth = new SwaggerClient.ApiKeyAuthorization(abp.auth.tokenHeaderName, 'Bearer ' + authToken, 'header');
        swaggerUi.api.clientAuthorizations.add('bearerAuth', cookieAuth);
        return true;
    }

    abp.swagger.addCsrfToken = function () {
        var csrfToken = abp.security.antiForgery.getToken();
        if (!csrfToken) {
            return false;
        }
        var csrfCookieAuth = new SwaggerClient.ApiKeyAuthorization(abp.security.antiForgery.tokenHeaderName, csrfToken, 'header');
        swaggerUi.api.clientAuthorizations.add(abp.security.antiForgery.tokenHeaderName, csrfCookieAuth);
        return true;
    }

    function loginUserInternal(callback) {
        var username = document.getElementById('userName').value;
        if (!username) {
            alert('账户不能为空');
            return false;
        }

        var password = document.getElementById('password').value;
        if (!password) {
            alert('密码不能为空');
            return false;
        }

        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    console.log(xhr.responseText)
                    var result = JSON.parse(xhr.responseText);
                    var expireDate = new Date(Date.now() + (result.Expire * 1000));
                    abp.auth.setToken(result.AccessToken, expireDate);
                    callback();
                } else {
                    alert('Login failed !');
                }
            }
        };

        xhr.open('POST', '/api/token/AuthenticateAsync', true);
        xhr.setRequestHeader('Content-type', 'application/json');
        xhr.send("{" + "UserName:'" + username + "'," + "PassWord:'" + password + "'}");
    };

    abp.swagger.login = function (callback) {
        loginUserInternal(callback); // Login for host
    };

    abp.swagger.logout = function () {
        abp.auth.clearToken();
    }

    abp.swagger.closeAuthDialog = function () {
        if (document.getElementById('abp-auth-dialog')) {
            document.getElementsByClassName("swagger-ui")[1].removeChild(document.getElementById('abp-auth-dialog'));
        }
    }

    abp.swagger.openAuthDialog = function (loginCallback) {
        abp.swagger.closeAuthDialog();

        var abpAuthDialog = document.createElement('div');
        abpAuthDialog.className = 'dialog-ux';
        abpAuthDialog.id = 'abp-auth-dialog';

        document.getElementsByClassName("swagger-ui")[1].appendChild(abpAuthDialog);

        // -- backdrop-ux
        var backdropUx = document.createElement('div');
        backdropUx.className = 'backdrop-ux';
        abpAuthDialog.appendChild(backdropUx);

        // -- modal-ux
        var modalUx = document.createElement('div');
        modalUx.className = 'modal-ux';
        abpAuthDialog.appendChild(modalUx);

        // -- -- modal-dialog-ux
        var modalDialogUx = document.createElement('div');
        modalDialogUx.className = 'modal-dialog-ux';
        modalUx.appendChild(modalDialogUx);

        // -- -- -- modal-ux-inner
        var modalUxInner = document.createElement('div');
        modalUxInner.className = 'modal-ux-inner';
        modalDialogUx.appendChild(modalUxInner);

        // -- -- -- -- modal-ux-header
        var modalUxHeader = document.createElement('div');
        modalUxHeader.className = 'modal-ux-header';
        modalUxInner.appendChild(modalUxHeader);

        var modalHeader = document.createElement('h3');
        modalHeader.innerText = 'Authorize';
        modalUxHeader.appendChild(modalHeader);

        // -- -- -- -- modal-ux-content
        var modalUxContent = document.createElement('div');
        modalUxContent.className = 'modal-ux-content';
        modalUxInner.appendChild(modalUxContent);

        modalUxContent.onkeydown = function (e) {
            if (e.keyCode === 13) {
                //try to login when user presses enter on authorize modal
                abp.swagger.login(loginCallback);
            }
        };

        //Inputs
        //createInput(modalUxContent, 'tenancyName', 'Tenancy Name (Leave empty for Host)');
        createInput(modalUxContent, 'userName', 'Username');
        createInput(modalUxContent, 'password', 'Password', 'password');

        //Buttons
        var authBtnWrapper = document.createElement('div');
        authBtnWrapper.className = 'auth-btn-wrapper';
        modalUxContent.appendChild(authBtnWrapper);

        //Close button
        var closeButton = document.createElement('button');
        closeButton.className = 'btn modal-btn auth btn-done button';
        closeButton.innerText = 'Close';
        closeButton.style.marginRight = '5px';
        closeButton.onclick = abp.swagger.closeAuthDialog;
        authBtnWrapper.appendChild(closeButton);

        //Authorize button
        var authorizeButton = document.createElement('button');
        authorizeButton.className = 'btn modal-btn auth authorize button';
        authorizeButton.innerText = 'Login';
        authorizeButton.onclick = function () {
            abp.swagger.login(loginCallback);
        };
        authBtnWrapper.appendChild(authorizeButton);
    }

    function createInput(container, id, title, type) {
        var wrapper = document.createElement('div');
        wrapper.className = 'wrapper';
        //if (id == "tenancyName") {
        //    wrapper.style.display = 'none';
        //}
        container.appendChild(wrapper);

        var label = document.createElement('label');
        label.innerText = title;
        wrapper.appendChild(label);

        var section = document.createElement('section');
        section.className = 'block-tablet col-10-tablet block-desktop col-10-desktop';
        wrapper.appendChild(section);

        var input = document.createElement('input');
        input.id = id;
        input.type = type ? type : 'text';
        input.style.width = '100%';


        section.appendChild(input);
    }

})();