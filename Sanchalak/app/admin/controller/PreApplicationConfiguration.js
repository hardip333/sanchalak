app.controller('PreApplicationConfigCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Pre-Application Configuration";
    // $scope.AppConfig = [];
    $scope.ApplicationConfigTableparam = new NgTableParams(
        {}, {
        dataset: $scope.AppConfig
    }); 

    $scope.resetAppConfig = function () {
        $scope.AppConfig = {};
        $scope.Feedetail = {};
        $scope.FEECONFIG = {};
        $scope.ApplicationFee = {};
        $scope.ShowFee = false;
        $scope.IsVisibleBtn = false;
    };

    $scope.getPreApplicationConfigById = function () {

       // alert($localStorage.localObj.ProgrammeInstancePartTerm);
        $scope.ProgrammeInstanceName = $localStorage.localObj.ProgInstanceName;
        $scope.ProgrammeName = $localStorage.localObj.ProgrammeName;
        $scope.PartName = $localStorage.localObj.PartName;
        $scope.ProgrammePartTermName = $localStorage.localObj.ProgrammePartTermName;

        $http({
            method: 'POST',
            url: 'api/AdmApplicationConfiguration/AdmApplicationConfigurationGetById',
            data: { ProgrammeInstancePartTermId: $localStorage.localObj.ProgrammeInstancePartTerm },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        //$scope.GetApplicationFeeDetail();
                        $scope.AppConfigList = response.obj;
                        //  console.log($scope.GroupList);
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.getPreApplicationConfig = function () {
      
        var data = new Object();
        //$scope.data.ApplicationStartDate='2020-02-02',
        //data.id = $rootScope.id;

        $http({
            method: 'POST',
            url: 'api/AdmApplicationConfiguration/AdmApplicationConfigurationGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ApplicationConfigTableparam = new NgTableParams(
                        {}, {
                        dataset: response.obj
                    });
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.addPreApplicationConfig = function () {

        if ($scope.AppConfig.Remarks == null || $scope.AppConfig.Remarks === undefined ||
            $scope.AppConfig.ProspectusURL == null || $scope.AppConfig.ProspectusURL === undefined || 
            //$localStorage.Feedetail.FeeCategoryId == null || $localStorage.Feedetail.FeeCategoryId === undefined ||
            //$localStorage.Feedetail.ProgrammeInstancePartTermId == null || $localStorage.Feedetail.ProgrammeInstancePartTermId === undefined
            $scope.FEECONFIG.ApplicationFeeId == null || $scope.FEECONFIG.ApplicationFeeId === undefined ) {
     
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.AppConfig.ApplicationStartDate == undefined || $scope.AppConfig.ApplicationStartDate == null) {
            alert("Please select Application Start Date.");
        }
        else if ($scope.AppConfig.ApplicationStopDate == undefined || $scope.AppConfig.ApplicationStopDate == null) {
            alert("Please select Application Stop Date.");
        }
        else if (($scope.AppConfig.ApplicationStopDate != null) && ($scope.AppConfig.ApplicationStartDate == undefined)) {
            alert("Please select Application Start Date.");
        }
        else if (($scope.AppConfig.ApplicationStartDate != null) && ($scope.AppConfig.ApplicationStopDate == undefined)) {
            alert("Please select Application End Date.");
        }

        else if (($scope.AppConfig.ApplicationFeeEndDate != null) && ($scope.AppConfig.ApplicationFeeStartDate == undefined)) {
            alert("Please select Application-Fees Start Date.");
        }
        else if (($scope.AppConfig.ApplicationFeeStartDate != null) && ($scope.AppConfig.ApplicationFeeEndDate == undefined)) {
            alert("Please select Application-Fees End Date.");
        }

        else if (($scope.AppConfig.AdmissionFeesStopDate != null) && ($scope.AppConfig.AdmissionFeesStartDate == undefined)) {
            alert("Please select Admission-Fees Start Date.");
        }
        else if (($scope.AppConfig.AdmissionFeesStartDate != null) && ($scope.AppConfig.AdmissionFeesStopDate == undefined)) {
            alert("Please select Admission-Fees End Date.");
        }

        else if (($scope.AppConfig.ApplicationApprovalStopDate != null) && ($scope.AppConfig.ApplicationApprovalStartDate == undefined)) {
            alert("Please select Application Approval Start Date.");
        }
        else if (($scope.AppConfig.ApplicationApprovalStartDate != null) && ($scope.AppConfig.ApplicationApprovalStopDate == undefined)) {
            alert("Please select Application Approval End Date.");
        }

        else if ($scope.AppConfig.ApplicationStartDate > $scope.AppConfig.ApplicationStopDate) {
            alert("Application Start Date should not be Greater than Application End Date.");
        }
        else if ($scope.AppConfig.ApplicationFeeStartDate > $scope.AppConfig.ApplicationFeeEndDate) {
            alert("Application-Fee Start Date should not be Greater than Application-Fee End Date.");
        }
        else if ($scope.AppConfig.AdmissionFeesStartDate > $scope.AppConfig.AdmissionFeesStopDate) {
            alert("Admission-Fees Start Date should not be Greater than Admission-Fees End Date.");
        }
        else if ($scope.AppConfig.ApplicationApprovalStartDate > $scope.AppConfig.ApplicationApprovalStopDate) {
            alert("Application Approval Start Date should not be Greater than Application Approval End Date.");
        }
        else if (($scope.AppConfig.ApplicationFeeStartDate != null) &&
            ($scope.AppConfig.ApplicationStartDate > $scope.AppConfig.ApplicationFeeStartDate)) {
            alert("Application Start Date should not be Greater than Application Fee Start Date.");
        }
        else if (($scope.AppConfig.ApplicationApprovalStartDate != null) &&
            ($scope.AppConfig.ApplicationStartDate > $scope.AppConfig.ApplicationApprovalStartDate)) {
            alert("Application Start Date should not be Greater than Application Approval Start Date.");
        }
        else {
            console.log($localStorage.Feedetail);
           
            $scope.AppConfig.ProgramInstancePartTermId = $localStorage.localObj.ProgrammeInstancePartTerm;
            $http({
                method: 'POST',
                url: 'api/AdmApplicationConfiguration/insertAdmApplicationConfiguration',
                data: $scope.AppConfig,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.resetAppConfig();
                        $scope.getPreApplicationConfigById();
                        //Jay's Code Start
                        console.log($scope.Feedetail);
                        $http({
                            method: 'POST',
                            url: 'api/FeeConfiguration/ApplicationFeeAdd',
                            data: $localStorage.Feedetail,
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
                                    $scope.Feedetail = {};
                                    $scope.FEECONFIG = {};
                                    $localStorage.Feedetail = {};
                                    $scope.ApplicationFee = {};
                                    $scope.ShowFee = false;

                                }
                            })
                            .error(function (res) {
                                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                            });


                    //Jay's Code End
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.modifyPreApplicationConfigData = function (data) {
        $scope.showFormFlag = true;
        $scope.AppConfig = data;
        $scope.getPreApplicationConfigById();

        var appStartDate = $scope.AppConfig.ApplicationStartDateView.split("-");
        $scope.AppConfig.ApplicationStartDate = new Date(appStartDate[2], (appStartDate[1] >= 1) ? (appStartDate[1] - 1) : appStartDate[1], appStartDate[0]);

        var appEndDate = $scope.AppConfig.ApplicationStopDateView.split("-");
        $scope.AppConfig.ApplicationStopDate = new Date(appEndDate[2], (appEndDate[1] >= 1) ? (appEndDate[1] - 1) : appEndDate[1], appEndDate[0]);

        var appFeeStartDate = $scope.AppConfig.ApplicationFeeStartDateView.split("-");
        $scope.AppConfig.ApplicationFeeStartDate = new Date(appFeeStartDate[2], (appFeeStartDate[1] >= 1) ? (appFeeStartDate[1] - 1) : appFeeStartDate[1], appFeeStartDate[0]);

        var appFeeEndDate = $scope.AppConfig.ApplicationFeeEndDateView.split("-");
        $scope.AppConfig.ApplicationFeeEndDate = new Date(appFeeEndDate[2], (appFeeEndDate[1] >= 1) ? (appFeeEndDate[1] - 1) : appFeeEndDate[1], appFeeEndDate[0]);

        var admStartDate = $scope.AppConfig.AdmissionFeesStartDateView.split("-");
        $scope.AppConfig.AdmissionFeesStartDate = new Date(admStartDate[2], (admStartDate[1] >= 1) ? (admStartDate[1] - 1) : admStartDate[1], admStartDate[0]);

        var admEndDate = $scope.AppConfig.AdmissionFeesStopDateView.split("-");
        $scope.AppConfig.AdmissionFeesStopDate = new Date(admEndDate[2], (admEndDate[1] >= 1) ? (admEndDate[1] - 1) : admEndDate[1], admEndDate[0]);

        var approvalStartDate = $scope.AppConfig.ApplicationApprovalStartDateView.split("-");
        $scope.AppConfig.ApplicationApprovalStartDate = new Date(approvalStartDate[2], (approvalStartDate[1] >= 1) ? (approvalStartDate[1] - 1) : approvalStartDate[1], approvalStartDate[0]);

        var approvalEndDate = $scope.AppConfig.ApplicationApprovalStopDateView.split("-");
        $scope.AppConfig.ApplicationApprovalStopDate = new Date(approvalEndDate[2], (approvalEndDate[1] >= 1) ? (approvalEndDate[1] - 1) : approvalEndDate[1], approvalEndDate[0]);

        var docEndDate = $scope.AppConfig.UpdatedDocLastDateView.split("-");
        $scope.AppConfig.UpdatedDocLastDate = new Date(docEndDate[2], (docEndDate[1] >= 1) ? (docEndDate[1] - 1) : docEndDate[1], docEndDate[0]);

        if ($scope.AppConfig.AdmInstallementEndDateView == null || $scope.AppConfig.AdmInstallementEndDateView == undefined || $scope.AppConfig.AdmInstallementEndDateView == "") {
            $scope.IsVisibleBtn = false;

        }

        else {
            var InstallementEndDate = $scope.AppConfig.AdmInstallementEndDateView.split("-");
            $scope.AppConfig.AdmInstallementEndDate = new Date(InstallementEndDate[2], (InstallementEndDate[1] >= 1) ? (InstallementEndDate[1] - 1) : InstallementEndDate[1], InstallementEndDate[0]);
            $scope.IsVisibleBtn = true;

        }
           
        
    };

    $scope.RemoveInstallementDate = function () {
     
        $scope.AppConfig.AdmInstallementEndDate = null;
        $scope.IsVisibleBtn = false;
        $scope.modifyPreApplicationConfig();

    }


    $scope.modifyPreApplicationConfig = function () { 

        if ($scope.AppConfig.Remarks == null || $scope.AppConfig.Remarks === undefined ||
            $scope.AppConfig.ProspectusURL == null || $scope.AppConfig.ProspectusURL === undefined ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please complete the form before Click...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.AppConfig.FeeSts == false &&( $scope.FEECONFIG.ApplicationFeeId == null || $scope.FEECONFIG.ApplicationFeeId === undefined)) {
            alert("Please select Fee Category");
        }
        else if (($scope.AppConfig.ApplicationStopDate != 'Invalid Date') && ($scope.AppConfig.ApplicationStartDate == 'Invalid Date')) {
            alert("Please select Application Start Date.");
        }
        else if (($scope.AppConfig.ApplicationStartDate != 'Invalid Date') && ($scope.AppConfig.ApplicationStopDate == 'Invalid Date')) {
            alert("Please select Application End Date.");
        }

        else if (($scope.AppConfig.ApplicationFeeEndDate != 'Invalid Date') && ($scope.AppConfig.ApplicationFeeStartDate == 'Invalid Date')) {
            alert("Please select Application-Fees Start Date.");
        }
        else if (($scope.AppConfig.ApplicationFeeStartDate != 'Invalid Date') && ($scope.AppConfig.ApplicationFeeEndDate == 'Invalid Date')) {
            alert("Please select Application-Fees End Date.");
        }

        else if (($scope.AppConfig.AdmissionFeesStopDate != 'Invalid Date') && ($scope.AppConfig.AdmissionFeesStartDate == 'Invalid Date')) {
            alert("Please select Admission-Fees Start Date.");
        }
        else if (($scope.AppConfig.AdmissionFeesStartDate != 'Invalid Date') && ($scope.AppConfig.AdmissionFeesStopDate == 'Invalid Date')){
            alert("Please select Admission-Fees End Date.");
        }

        else if (($scope.AppConfig.ApplicationApprovalStopDate != 'Invalid Date') && ($scope.AppConfig.ApplicationApprovalStartDate == 'Invalid Date')) {
            alert("Please select Application Approval Start Date.");
        }
        else if (($scope.AppConfig.ApplicationApprovalStartDate != 'Invalid Date') && ($scope.AppConfig.ApplicationApprovalStopDate == 'Invalid Date')) {
            alert("Please select Application Approval End Date.");
        }

        else if ($scope.AppConfig.ApplicationStartDate > $scope.AppConfig.ApplicationStopDate) {
            alert("Application Start Date should not be Greater than Application End Date.");
        }
        else if ($scope.AppConfig.ApplicationFeeStartDate > $scope.AppConfig.ApplicationFeeEndDate) {
            alert("Application-Fee Start Date should not be Greater than Application-Fee End Date.");
        }
        else if ($scope.AppConfig.AdmissionFeesStartDate > $scope.AppConfig.AdmissionFeesStopDate) {
            alert("Admission-Fees Start Date should not be Greater than Admission-Fees End Date.");
        }
        else if ($scope.AppConfig.ApplicationApprovalStartDate > $scope.AppConfig.ApplicationApprovalStopDate) {
            alert("Application Approval Start Date should not be Greater than Application Approval End Date.");
        }
        
        else if (($scope.AppConfig.ApplicationFeeStartDate != 'Invalid Date' && $scope.AppConfig.ApplicationFeeStartDate != null) &&
            ($scope.AppConfig.ApplicationStartDate > $scope.AppConfig.ApplicationFeeStartDate)) {
            alert("Application Start Date should not be Greater than Application Fee Start Date.");
        }
        else if (($scope.AppConfig.ApplicationApprovalStartDate != 'Invalid Date' && $scope.AppConfig.ApplicationApprovalStartDate != null) &&
            ($scope.AppConfig.ApplicationStartDate > $scope.AppConfig.ApplicationApprovalStartDate)) {
            alert("Application Start Date should not be Greater than Application Approval Start Date.");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/AdmApplicationConfiguration/updateAdmApplicationConfiguration',
                data: $scope.AppConfig,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        alert(response.obj);
                        $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    } else {
                        alert(response.obj);
                        //$scope.AppConfig = {};
                        $scope.showFormFlag = false;
                        //Jay's Code Start
                        if ($scope.AppConfig.FeeSts == false) {
                            $http({
                                method: 'POST',
                                url: 'api/FeeConfiguration/ApplicationFeeAdd',
                                data: $localStorage.Feedetail,
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
                                        $scope.Feedetail = {};
                                        $scope.FEECONFIG = {};
                                        $localStorage.Feedetail = {};
                                        $scope.ApplicationFee = {};
                                        $scope.ShowFee = false;

                                    }
                                })
                                .error(function (res) {
                                    $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                                });
                        }

                    //Jay's Code End
                        //$scope.checkAppStart = false;
                        //if (response.obj == "APP_START_DATE_NO_CHANGE") {
                        //    $scope.checkAppStart = true;
                        //}
                        //alert($scope.checkAppStart);
                        $scope.getPreApplicationConfigById();
                    }
                })
                .error(function (res) {
                    alert(res.obj);
                    $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.deletePreApplicationConfig = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.AppConfig = data;

            $http({
                method: 'POST',
                url: 'api/AdmApplicationConfiguration/deleteAdmApplicationConfiguration',
                data: $scope.AppConfig,
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
                        $scope.getPreApplicationConfigById();
                        //location.reload();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
            alert($scope.status);
        });
    };


    $scope.newAppConfigAdd = function () {
        $state.go('PreApplicationConfigurationAdd');
    };

    $scope.backToListAppConfig = function () {
        $state.go('PreApplicationConfigurationEdit');
    };

    $scope.displayAppConfig = function (data) {
        $scope.AppConfig = data;
    };

    //Jay Code Start
    $scope.getApplicationFee = function () {
        $scope.FEECONFIG = {};
        $scope.FEECONFIG.ProgrammeInstancePartTermId = $localStorage.localObj.ProgrammeInstancePartTerm;
        console.log($scope.FEECONFIG);
        $http({
            method: 'POST',
            url: 'api/FeeConfiguration/ApplicationFeeGet',
            data: $scope.FEECONFIG,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ApplicationFee = response.obj;
                    $scope.ShowFee = false;

                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    $scope.ShowLableFeeDetail = function () {
        $scope.Feedetail = {};
        for (var i in $scope.ApplicationFee) {
            if ($scope.FEECONFIG.ApplicationFeeId == $scope.ApplicationFee[i].Id) {
                $scope.Feedetail = $scope.ApplicationFee[i];
                $scope.Feedetail.ProgrammeInstancePartTermId = $localStorage.localObj.ProgrammeInstancePartTerm;
                $scope.ShowFee = true;
            }
        }
        $localStorage.Feedetail =  $scope.Feedetail;
    };
    
    
    //Jay Code end

});
