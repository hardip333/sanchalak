app.controller('EmployeeRegistrationCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage MstPaper Details";

    $scope.UpdateBtnFlag = false;
    /*Reset MstEmployee*/
    $scope.resetMstEmployee = function () {
        $scope.EmpReg = {};
        $scope.UpdateBtnFlag = false;
    };

    //$scope.EmpReg.IsCurrentAsPermanent = false;
   
    $("#adharnE").on("keyup", function () {

        var adharnE = $("#adharnE").val();
        var aadharCheck = document.getElementById("aadharCheck");

        adharnE = adharnE.replace(/\D/g, "").split(/(?:([\d]{4}))/g).filter(s => s.length > 0).join("-");
        $(this).val(adharnE);

        var regexp = /^[2-9]{1}[0-9]{3}\-{1}[0-9]{4}\-{1}[0-9]{4}$/;
        if (regexp.test(adharnE)) {

            aadharCheck.innerHTML = "";
        }
        else {

            aadharCheck.innerHTML = "Enter valid Aadhar Number";
        }
    });
    $("#panId").on("keyup", function () {

        var panId = $("#panId").val();
        var panCheck = document.getElementById("panCheck");


        var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
        if (regpan.test(panId)) {

            panCheck.innerHTML = "";
        }
        else {

            panCheck.innerHTML = "Enter valid PAN Number";
        }
    });
  
    //Function for expand columns in row click
    $scope.expand_row = function (id) {
        let element = document.getElementById('expand' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + id).innerHTML = "+"
            element.add("collapse")
        }
    }

    $scope.NatureOfAppointmentList = [
        {
            id: '1',
            name: 'Permanent'
        }, 

         {
            id: '2',
            name: 'Temporary'
        }];  

    $scope.resetEmpReg = function () {
        $scope.EmpReg = {};
    };

    $scope.GenReservationCategoryGet = function () {

        $http({
            method: 'GET',
            url: 'api/EmployeeRegistration/GenReservationCategoryGet',
            data: $scope.EmpReg,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.SocialCategoryList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.UserMasterGet = function () {
       
        $http({
            method: 'POST',
            url: 'api/EmployeeRegistration/UserTypeMasterGet',
            data: $scope.EmpReg,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.UserTypeList = response.obj;
                //console.log($scope.UserTypeList);
            })
            .error(function (res) {
                alert(res);
            });

    };
    $scope.BloodGroupGet = function () {
        
        $http({
            method: 'POST',
            url: 'api/EmployeeRegistration/BloodGroupGet',
            data: $scope.ApplicantRegistration,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.bloodGrpList = response.obj;
               
            })
            .error(function (res) {
                alert(res);
            });

    };
    $scope.MaritalStatusGet = function () {
      
        $http({
            method: 'POST',
            url: 'api/EmployeeRegistration/MaritalStatusGet',
            data: $scope.EmpReg,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.martialList = response.obj;
                //console.log($scope.martialList);
                
            })
            .error(function (res) {
                alert(res);
            });

    };
    $scope.CountryMasterGet = function () {
      
        $http({
            method: 'POST',
            url: 'api/EmployeeRegistration/CountryMasterGet',
            data: $scope.ResidentialAddressSaved,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.cmList = response.obj;
               
               
            })
            .error(function (res) {
                alert(res);
            });

    };

    $scope.StateMasterGet = function () {
        alert($scope.EmpReg.CurrentCountryId);
        //$scope.ResidentialAddressSaved.PermanentStateId = 0;
        //$scope.ResidentialAddressSaved.PermanentDistrictId = 0;

        var CountryId = {
            Id: $scope.EmpReg.CurrentCountryId
        }

        $http({
            method: 'POST',
            url: 'api/EmployeeRegistration/StateMasterGet',
            data: CountryId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.stateMstList = response.obj;
               
            })
            .error(function (res) {
                alert(res);
            });

        console.log(CountryId);
       
    };

    $scope.DistrictMasterGet = function () {
       
        var StateId = {
            Id: $scope.EmpReg.CurrentStateId
        }
      
        $http({
            method: 'POST',
            url: 'api/EmployeeRegistration/DistrictMasterGet',
            data: StateId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.distMstList = response.obj;
               
            })
            .error(function (res) {
                alert(res);
            });
        
    };
    $scope.StateMasterSGet = function () {
       
        /*$scope.ResidentialAddressSaved.CurrentStateId = 0;
        $scope.ResidentialAddressSaved.CurrentDistrictId = 0;*/

        var CountryId = {
            Id: $scope.EmpReg.PermanentCountryId
        }

        $http({
            method: 'POST',
            url: 'api/EmployeeRegistration/StateMasterSGet',
            data: CountryId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.smsList = response.obj;
                
            })
            .error(function (res) {
                alert(res);
            });
        
    };

    $scope.DistrictMasterDGet = function () {
       
        var StateId = {
            Id: $scope.EmpReg.PermanentStateId
        }

        $http({
            method: 'POST',
            url: 'api/EmployeeRegistration/DistrictMasterDGet',
            data: StateId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.dmdList = response.obj;
                
            })
            .error(function (res) {
                alert(res);
            });

    };

    $scope.EmpReg = {};
    $scope.IsCurrentAsPermanent = {};
    $scope.PermanentAddress = {};

    $scope.addMstEmployee = function () {

        var ddlUserType = document.getElementById("ddlUserType");

           //$scope.EmpReg.NatureOfAppointment = $scope.NatureOfAppointment.name;
        if (ddlUserType.value === null || ddlUserType.value === undefined ||
            $scope.EmpReg.FirstName === null || $scope.EmpReg.FirstName === undefined ||
            $scope.EmpReg.MiddleName === null || $scope.EmpReg.MiddleName === undefined ||
            $scope.EmpReg.LastName === null || $scope.EmpReg.LastName === undefined ||
            $scope.EmpReg.MobileNo === null || $scope.EmpReg.MobileNo === undefined ||
            $scope.EmpReg.EmailId === null || $scope.EmpReg.EmailId === undefined ||
            $scope.EmpReg.BirthDate === null || $scope.EmpReg.BirthDate === undefined ||
            $scope.EmpReg.SocialCategoryId === null || $scope.EmpReg.SocialCategoryId === undefined ||
            $scope.EmpReg.MaritalStatusId === null || $scope.EmpReg.MaritalStatusId === undefined ||
            $scope.EmpReg.BloodGroupId === null || $scope.EmpReg.BloodGroupId === undefined ||
            $scope.EmpReg.CurrentAddress === null || $scope.EmpReg.CurrentAddress === undefined ||
            $scope.EmpReg.CurrentCountryId === null || $scope.EmpReg.CurrentCountryId === undefined ||
            $scope.EmpReg.CurrentStateId === null || $scope.EmpReg.CurrentStateId === undefined||
            $scope.EmpReg.CurrentDistrictId === null || $scope.EmpReg.CurrentDistrictId === undefined ||
            $scope.EmpReg.CurrentCityVillage === null || $scope.EmpReg.CurrentCityVillage === undefined ||
            $scope.EmpReg.CurrentPincode === null || $scope.EmpReg.CurrentPincode === undefined ||
            $scope.EmpReg.AadharCardNo === null || $scope.EmpReg.AadharCardNo === undefined ||
            $scope.EmpReg.VoterId === null || $scope.EmpReg.VoterId === undefined ||
            $scope.EmpReg.PanCardNo === null || $scope.EmpReg.PanCardNo === undefined )
            //$scope.EmpReg.NatureOfAppointment === null || $scope.EmpReg.NatureOfAppointment === undefined)
            {
                alert("please check all required fields !!!");
            }
            else if ($scope.EmpReg.IsCurrentAsPermanent)
            {
                $scope.EmpReg.PermanentAddress = $scope.EmpReg.CurrentAddress;
                $scope.EmpReg.PermanentCountryId = $scope.EmpReg.CurrentCountryId;
                $scope.EmpReg.PermanentStateId = $scope.EmpReg.CurrentStateId;
                $scope.EmpReg.PermanentDistrictId = $scope.EmpReg.CurrentDistrictId;
                $scope.EmpReg.PermanentCityVillage = $scope.EmpReg.CurrentCityVillage;
                $scope.EmpReg.PermanentPincode = $scope.EmpReg.CurrentPincode;

                $http({
                    method: 'POST',
                    url: 'api/EmployeeRegistration/MstEmployeeAdd',
                    data: $scope.EmpReg,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {

                        $rootScope.showLoading = false;
                        if (response.response_code == "0") {
                            //Redirect user to login page
                            $state.go('login');
                            //return false;
                        } else if (response.response_code != "200") {
                            // alert(response.obj);
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            //alert("your data added successfully");
                            alert(response.obj);
                            $scope.EmpReg = {};
                            //$scope.NatureOfAppointmentList = {};
                            $scope.MstEmployeeGet();
                      
                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });

            }
       
            else if ($scope.EmpReg.PermanentAddress === null || $scope.EmpReg.PermanentAddress === undefined ||
                    $scope.EmpReg.PermanentCountryId === null || $scope.EmpReg.PermanentCountryId === undefined ||
                    $scope.EmpReg.PermanentStateId === null || $scope.EmpReg.PermanentStateId === undefined ||
                    $scope.EmpReg.PermanentDistrictId === null || $scope.EmpReg.PermanentDistrictId === undefined ||
                    $scope.EmpReg.PermanentCityVillage === null || $scope.EmpReg.PermanentCityVillage === undefined ||
                    $scope.EmpReg.PermanentPincode === null || $scope.EmpReg.PermanentPincode === undefined)
                    {
                        alert("please check it");
                    }
             else
               {
             
                
                $http({
                    method: 'POST',
                    url: 'api/EmployeeRegistration/MstEmployeeAdd',
                    data: $scope.EmpReg,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {

                        $rootScope.showLoading = false;
                        if (response.response_code == "0") {
                            //Redirect user to login page
                            $state.go('login');
                            //return false;
                        } else if (response.response_code != "200") {
                            // alert(response.obj);
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            //alert("your data added successfully");
                            alert(response.obj);
                            $scope.EmpReg = {};
                            $scope.MstEmployeeGet();
                        }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
          
            }
       
            
    };

    /*Get MstEmployee List*/
    $scope.MstEmployeeGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/EmployeeRegistration/MstEmployeeGet',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.MstEmployeeTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.MstEmployeeData = response.obj;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Modify MstEmployee Data*/
    $scope.modifyMstEmployeeData = function (data) {
        $scope.showFormFlag = true;
        $scope.EmpReg  = data;
        $(window).scrollTop(0);
        $scope.UpdateBtnFlag = true;
        $scope.StateMasterGet();
        $scope.DistrictMasterGet();
        $scope.DistrictMasterGet();

        var BirthDate = $scope.EmpReg.BirthDateView.split("-");
        $scope.EmpReg.BirthDate = new Date(BirthDate[2], (BirthDate[1] >= 1) ? (BirthDate[1] - 1) : BirthDate[1], BirthDate[0]);

    };

    /*Update MstEmployee Data*/
    $scope.MstEmployeeEdit = function () {
        var ddlUserType = document.getElementById("ddlUserType");
        if (ddlUserType.value === null || ddlUserType.value === undefined ||
            $scope.EmpReg.FirstName === null || $scope.EmpReg.FirstName === undefined ||
            $scope.EmpReg.MiddleName === null || $scope.EmpReg.MiddleName === undefined ||
            $scope.EmpReg.LastName === null || $scope.EmpReg.LastName === undefined ||
            $scope.EmpReg.MobileNo === null || $scope.EmpReg.MobileNo === undefined ||
            $scope.EmpReg.EmailId === null || $scope.EmpReg.EmailId === undefined ||
            $scope.EmpReg.BirthDate === null || $scope.EmpReg.BirthDate === undefined ||
            $scope.EmpReg.SocialCategoryId === null || $scope.EmpReg.SocialCategoryId === undefined ||
            $scope.EmpReg.MaritalStatusId === null || $scope.EmpReg.MaritalStatusId === undefined ||
            $scope.EmpReg.BloodGroupId === null || $scope.EmpReg.BloodGroupId === undefined ||
            $scope.EmpReg.CurrentAddress === null || $scope.EmpReg.CurrentAddress === undefined ||
            $scope.EmpReg.CurrentCountryId === null || $scope.EmpReg.CurrentCountryId === undefined ||
            $scope.EmpReg.CurrentStateId === null || $scope.EmpReg.CurrentStateId === undefined ||
            $scope.EmpReg.CurrentDistrictId === null || $scope.EmpReg.CurrentDistrictId === undefined ||
            $scope.EmpReg.CurrentCityVillage === null || $scope.EmpReg.CurrentCityVillage === undefined ||
            $scope.EmpReg.CurrentPincode === null || $scope.EmpReg.CurrentPincode === undefined ||
            $scope.EmpReg.AadharCardNo === null || $scope.EmpReg.AadharCardNo === undefined ||
            $scope.EmpReg.VoterId === null || $scope.EmpReg.VoterId === undefined ||
            $scope.EmpReg.PanCardNo === null || $scope.EmpReg.PanCardNo === undefined)
        //$scope.EmpReg.NatureOfAppointment === null || $scope.EmpReg.NatureOfAppointment === undefined)
        {
            alert("please check all required fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/EmployeeRegistration/MstEmployeeEdit',
                data: $scope.EmpReg ,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.resetMstEmployee();
                        $scope.MstEmployeeGet();
                        $scope.EmpReg = {};
                        //$scope.NatureOfAppointmentList = {};
                        $scope.showFormFlag = false;
                        $scope.UpdateBtnFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete MstDesignation Data*/
    $scope.MstEmployeeDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.EmpReg = data;

            $http({
                method: 'POST',
                url: 'api/EmployeeRegistration/MstEmployeeDelete',
                data: $scope.EmpReg,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.MstEmployeeGet();
                        $scope.EmpReg = {};
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

});

app.directive('allowPattern', [allowPatternDirective]);

function allowPatternDirective() {
    return {
        restrict: "A",
        compile: function (tElement, tAttrs) {
            return function (scope, element, attrs) {

                element.bind("keypress", function (event) {
                    var keyCode = event.which || event.keyCode;
                    var keyCodeChar = String.fromCharCode(keyCode);

                    if (!keyCodeChar.match(new RegExp(attrs.allowPattern, "i"))) {
                        event.preventDefault();
                        return false;
                    }

                });
            };
        }
    };
}