var APIUrl = "";
var iNumLights = 500;
var iMaxPollTimer = 60000;
var iCalls = 0;
var vStartTime;


function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function ColorTrigger(x) {

    $.ajax({
        url: APIUrl,
        global: true,
        success: function (data) {

            iCalls++;
            var vInterval = Date.now() - vStartTime;
            var vRate = (1000 * iCalls / vInterval).toFixed(2)
            $('#idCallsText').html("Calls:&nbsp;" + iCalls + "&nbsp;&nbsp;Rate: " + vRate + " ps");

            var colorName = data.name;
            $('#idLight' + x.toString()).css({ fill: colorName });

            var iDelay = Math.ceil(Math.random() * iMaxPollTimer);
            setTimeout(function (xx) { ColorTrigger(xx); }, iDelay, x);

        }
    });
}

function doStart() {

    $("#idButStart").hide();
    $("#idButReset").show();

    iCalls = 0;
    vStartTime = Date.now();
    var iDelay = 0;
    for (var i = 0; i < iNumLights; i++) {
        iDelay = Math.ceil(Math.random() * iMaxPollTimer);
        setTimeout(function (xx) { ColorTrigger(xx); }, iDelay, i);
    }
}

function doDefaultReady() {
    var vHTML = "";

    APIMode = decodeURIComponent(readCookie("APIMode"));
    if (APIMode === "Direct") {
        APIUrl = decodeURIComponent(readCookie("APIUrl"));
        $("#idAPIUrl").html(APIUrl);
    }
    else {
        APIUrl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '') + "/getcolor";
        $("#idAPIUrl").html(APIUrl + " &#10148; " + decodeURIComponent(readCookie("APIUrl")));
    }

    iNumLights = parseInt(decodeURIComponent(readCookie("NumberLights")));
    if (isNaN(iNumLights)) {
        iNumLights = 500;
        $('#idErrorText').html("Need to configure");
    }

    for (var i = 0; i < iNumLights; i++) {
        vHTML += "<svg height='30' width='30'><circle id='idLight" + i.toString() + "' cx='15' cy='15' r='12' stroke='black' stroke-width='1' fill='white' /></svg>";
    }

    $("#idLights").append(vHTML);


    $(document).ajaxError(
        function (e, x, settings, exception) {
            var message;
            if (x.status) {
                message = "HTTP Error: " + x.status;
            } else {
                message = "Failure: " + exception;
            }
            $('#idErrorText').html(message);
        }
    );
}

function openNav() {

    $("#idnavoverlay").css("display", "block");
    $("#idnavsidebar").css("marginLeft", "0px");
    $("#idnavsidebar").css("marginRight", "0px");
    $("#idmaincontent").css("marginLeft", "250px");
    $("#idmaincontent").css("marginRight", "-250px");
}

function closeNav() {

    $("#idnavsidebar").css("marginLeft", "-250px");
    $("#idnavsidebar").css("marginRight", "250px");
    $("#idmaincontent").css("marginLeft", "0");
    $("#idmaincontent").css("marginRight", "0");
    $("#idnavoverlay").css("display", "none");
}

function toggleNav() {
    if ($("#idnavbutton").hasClass("is-active"))
        closeNav();
    else
        openNav();
    $("#idnavbutton").toggleClass("is-active");
}

function gotoNav(href) {
    toggleNav();
    window.setTimeout("window.location.href='" + href + "'", 500);
}

