app.controller('ResExemptionConfigCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, $localStorage, NgTableParams) {
   
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Result Course Exemption Configuration";
    $scope.ResExemtion = {};

    //$scope.CheckTotalMarks = function () {
    //    console.log($scope.PaperListforResExemptionConfig);
    //    debugger
    //   // for (let i = 0; (i < $scope.PaperListforResExemptionConfig[i].ExemptionMarks) && ($scope.PaperListforResExemptionConfig[i].ExemptionMarks != null || $scope.PaperListforResExemptionConfig[i].ExemptionMarks != undefined); i++) {
    //    for (var i in $scope.PaperListforResExemptionConfig[i].ExemptionMarks) {
    //        if ($scope.PaperListforResExemptionConfig[i].ExemptionMarks >= 100) {
    //            alert("Please Add marks below 100..!!");
    //        }
    //    }
    //};

    $scope.AcademicYearGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/ResExemptionConfig/AcademicYearGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.AcademicList = response.obj;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.FacultyGet = function () {
        $scope.FacultyList = {};
        $http({
            method: 'POST',
            url: 'api/ResExemptionConfig/FacultyGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.FacultyList = {};

                    }
                }
                else {
                    $scope.FacultyList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    $scope.getProgrammeInstanceListByAcadId = function () {
        $scope.InstList = {};
        $http({
            method: 'POST',
            url: 'api/ResExemptionConfig/InstanceListGetbyFacultyIdAndAcadId',
            data: $scope.ResExemtion,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.InstList = {};

                    }
                }
                else {
                    $scope.InstList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    $scope.getProgrammePartListByProgInstId = function () {
        //$scope.FEECONFIG.ProgrammeInstanceId = $scope.FEECONFIG.ProgrammeInstanceId;


        $scope.ProgPartList = {};
        $http({
            method: 'POST',
            url: 'api/ResExemptionConfig/ProgrammePartGetByProgInstId',
            data: $scope.ResExemtion,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ProgPartList = {};

                    }
                }
                else {
                    $scope.ProgPartList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.getProgPartTermListByProgInstPartId = function () {
        $scope.ProgPartTermList = {};
        $http({
            method: 'POST',
            url: 'api/ResExemptionConfig/ProgrammePartTermGetByProgInstId',
            data: $scope.ResExemtion,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.ProgPartTermList = {};

                    }
                }
                else {
                    $scope.ProgPartTermList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getPaperListforResExemptionConfig = function () {

        $http({
            method: 'POST',
            url: 'api/ResExemptionConfig/FinalViewforExemptionConfig',
            data: $scope.ResExemtion,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    
                    
                    $scope.PaperListforResExemptionConfig = response.obj;

                    for (var i = 0; i < $scope.PaperListforResExemptionConfig.length; i++) {
                        if ($scope.PaperListforResExemptionConfig[i].IsLaunched == true) {
                            $scope.deleteExemConfig = true;
                        }

                    }
                    
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }



    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    $scope.addResExemption = function () {
        /*$scope.CheckTotalMarks();*/
        debugger;
        $scope.ResExemtion.PaperList1 = $scope.PaperListforResExemptionConfig;
        //$scope.ResExemtion.IncProgrammeInstancePartTermId = $scope.PaperListforResExemptionConfig[0].IncProgrammeInstancePartTermId;
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/ResExemptionConfig/ResExemptionConfigAdd',
            data: $scope.ResExemtion,
            eaders: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);

                    if (response.response_code == "201") {
                        alert("ExemptionMarksPercent/ExemptionMarks should be below 100");
                        $scope.offSpinner();
                    }
                }
                else {
                    $scope.offSpinner();
                    alert(response.obj);
                    $scope.PaperListforResExemptionConfig = {};
                    $scope.ResExemtion = {};

                }
            })
            .error(function (res) {
                alert(res.obj);
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }


    $scope.deleteResExemption = function (ev) {
        if ($scope.PaperListforResExemptionConfig == null || $scope.PaperListforResExemptionConfig === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#ResExemption')))
                    .clickOutsideToClose(true)
                    .title("Message")
                    .textContent("There is No Record")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            var confirm = $mdDialog.confirm()
                .title('Would you like to delete?')
                .textContent('Record will be deleted permanently.')
                .ariaLabel('Lucky day')
                .targetEvent(ev)
                .ok('Yes')
                .cancel('No');
            $mdDialog.show(confirm).then(function () {

                $scope.ResExemtion.PaperList1 = $scope.PaperListforResExemptionConfig;
                // $scope.GTempConfig = data;
                $scope.onSpinner();
                $http({
                    method: 'POST',
                    url: 'api/ResExemptionConfig/ResExemptionDelete',
                    data: $scope.ResExemtion,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;
                        if (response.response_code == "0") {
                            $state.go('login');
                        }
                        else
                            if (response.response_code != "200") {
                                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                            }
                            else {
                                $scope.offSpinner();
                                alert(response.obj);
                                $scope.ResExemtion = {};
                                $scope.PaperListforResExemptionConfig = {};
                                
                                

                            }
                    })
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });

            },
                function () {
                    $scope.status = 'You decided to keep your debt.';
                });
        }
    };

    $scope.LaunchResConfiguration = function (data) {


        $http({
            method: 'POST',
            url: 'api/ResExemptionConfig/ResultConfigurationLaunch',
            data: $scope.ResExemtion,
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

                    /*$scope.getLaunchPartTermByFacIdAndAcadId();*/

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };







});