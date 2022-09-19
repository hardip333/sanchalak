app.controller('ApplicantInformationGetCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    var favoriteCookie = $cookies.get('token');

    if (!favoriteCookie) {
        $state.go('login');
    }
    $scope.RetrieveInformation = {};
    $scope.RetrievedInfo = {};

    $scope.boldString = function (str, find) {
        var re = new RegExp(find, 'g');
        return str.toString().replace(re, '<b>' + find + '</b>');
    }

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    //Get Information by Username or Firstname or Middlename or Lastname or Mobile or Email  ---------- START------------  
    $scope.ApplicantInformationGet = function (username) {
        $scope.RetrieveInformation.UserName = username;
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/Verification/RetriveDecodedPassword',
            data: { UserName: $scope.RetrieveInformation.UserName},
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    alert(response.obj);
                    $scope.offSpinner();
                    $scope.InfoVisibleFlag = false;
                }
                else {
                    $scope.RetrievedInfo = response.obj;
                    $scope.offSpinner();
                    var result = $scope.boldString($scope.RetrievedInfo, $scope.RetrieveInformation.UserName);
                    console.log(result);
                    if (document.getElementById("RetrivedInfo") != null) {
                        
                        document.getElementById("RetrivedInfo").innerHTML = result;
                       
                    }
                    $scope.InfoVisibleFlag = true;
                   
                    
                }
                
            })
            .error(function (res) {
                alert(res);
            });
    };  
    //Get Information by Username or Firstname or Middlename or Lastname or Mobile or Email  ---------- END------------


    //View Full Details Of Applicant START
    $scope.RedirectToApplicantAdmissionDetails = function (data) {
        $localStorage.ApplicantInfo = {};
        $localStorage.ApplicantInfo.UserName = data;
        $localStorage.ApplicantInfo.FlagFromApplicantInfo = true;
       
        
    };

    //View Full Details Of Applicant END
});



