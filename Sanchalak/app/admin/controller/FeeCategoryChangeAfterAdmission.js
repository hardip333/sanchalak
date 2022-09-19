app.controller('FeeCategoryChangeAfterAdmissionController', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {
    debugger;
    $scope.visibility = true;
    $scope.elig = true;
    $scope.progpartterm = true;
    $scope.filter = {};
    $scope.filter.radioValue = "Part";
    //document.getElementById('admissionlevel').style.display = "none";
    //$scope.visibile = true;
    //document.getElementById('vis').style.display = "none";
    $scope.ShowTable = false;
    //$rootScope.pageTitle = "Manage Create Table";
    debugger;
    //$scope.ShowDate = false;
    $rootScope.showLoading = false;

    $scope.getProgList = function () {
        debugger;
        $http({
            method: 'POST',
            url: 'api/FeeCategoryChangeAfterAdmission/getProgList',
            //data: DATA,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    debugger;
                    $scope.ProgList = response.obj;
                    //var lengthOfTTData = Object.keys($scope.TTData).length;
                    ////var lengthOfTTData = getObjectLength($scope.TTDataToBePassed);
                    //for (var i = 0; i < lengthOfTTData; i++) {
                    //    var elem = document.getElementById('submit');
                    //    if ($scope.TTData[i].buttonVisibility == 1) {
                    //        elem.style.display = "block";
                    //    }
                    //}

                    //$localStorage.TTData = $scope.TTData;
                    //$scope.TTC = DATA;
                    //$scope.TTC.TTData = $scope.TTData;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getProgPartfromProg = function (ProgId) {
        debugger;
        $http({
            method: 'POST',
            url: 'api/FeeCategoryChangeAfterAdmission/getProgPartfromProg?ProgId=' + ProgId,
            //data: DATA,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    debugger;
                    $scope.ProgPartList = response.obj;
                    //var lengthOfTTData = Object.keys($scope.TTData).length;
                    ////var lengthOfTTData = getObjectLength($scope.TTDataToBePassed);
                    //for (var i = 0; i < lengthOfTTData; i++) {
                    //    var elem = document.getElementById('submit');
                    //    if ($scope.TTData[i].buttonVisibility == 1) {
                    //        elem.style.display = "block";
                    //    }
                    //}

                    //$localStorage.TTData = $scope.TTData;
                    //$scope.TTC = DATA;
                    //$scope.TTC.TTData = $scope.TTData;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getProgInstPartTermfromProgPart = function (fulldata) {
        debugger;
        $http({
            method: 'POST',
            url: 'api/FeeCategoryChangeAfterAdmission/getProgInstPartTermfromProgPart',
            data: fulldata,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    debugger;
                    $scope.ProgInstPartTermList = response.obj;
                    //var lengthOfTTData = Object.keys($scope.TTData).length;
                    ////var lengthOfTTData = getObjectLength($scope.TTDataToBePassed);
                    //for (var i = 0; i < lengthOfTTData; i++) {
                    //    var elem = document.getElementById('submit');
                    //    if ($scope.TTData[i].buttonVisibility == 1) {
                    //        elem.style.display = "block";
                    //    }
                    //}

                    //$localStorage.TTData = $scope.TTData;
                    //$scope.TTC = DATA;
                    //$scope.TTC.TTData = $scope.TTData;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //$scope.getSpecFromProgPartTerm = function (ProgPartTermId) {
    //    debugger;
    //    $http({
    //        method: 'POST',
    //        url: 'api/FeeCategoryChangeAfterAdmission/getSpecFromProgPartTerm?ProgPartTermId=' + ProgPartTermId,
    //        //data: DATA,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            if (response.response_code == "0") {
    //                $state.go('login');
    //            }
    //            if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                debugger;
    //                $scope.Branchlist = response.obj;
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};


    //$scope.checkAlreadyExistingRecords = function (alldata) {
    //    debugger;
    //    $localStorage.alldata1 = alldata;
    //    //if (alldata.radioValue == "Part") {
    //    //    $scope.Name = $("#partName option:selected").text()
    //    //}
    //    //else if (alldata.radioValue == "PartTerm") {
    //    //    $scope.Name = $("#ptName option:selected").text()
    //    //}
    //    //else {
    //    //    alert('Select a level of configuration');
    //    //    $state.go('createtimetable');
    //    //}

    //    //alert($("#partName option:selected").text());
    //    //$scope.visibility = true;
    //    $http({
    //        method: 'POST',
    //        url: 'api/EligibilityCriteriaConfiguration/checkAlreadyExistingRecords',
    //        data: alldata,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            if (response.response_code == "0") {
    //                $state.go('login');
    //            }
    //            if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                debugger;
    //                $scope.ExistingData = response.obj;
    //                if (response.obj.length == 0) {
    //                    $scope.visibility = true;
    //                    $scope.visibile = false;
    //                    //$localStorage.configLevel = "";
    //                    alert('Please select a level of configuration');
    //                }
    //                else {
    //                    $scope.FirstRecord = $scope.ExistingData[0];
    //                    $localStorage.configLevel = $scope.FirstRecord.TypeforEligibilty;
    //                    $localStorage.length = response.obj.length;
    //                    $localStorage.ExistingData = $scope.ExistingData;
    //                    //document.getElementById('vis').style.display = "none";
    //                    document.getElementById('vis1').style.display = "block";
    //                    //$scope.buttvisibility = false;
    //                    //$scope.buttonvisibility = false;
    //                    //var lengthOfTTData = Object.keys($scope.TTData).length;
    //                    ////var lengthOfTTData = getObjectLength($scope.TTDataToBePassed);
    //                    //for (var i = 0; i < lengthOfTTData; i++) {
    //                    //    var elem = document.getElementById('submit');
    //                    //    if ($scope.TTData[i].buttonVisibility == 1) {
    //                    //        elem.style.display = "block";
    //                    //    }
    //                    //}

    //                    //$localStorage.TTData = $scope.TTData;
    //                    //$scope.TTC = DATA;
    //                    //$scope.TTC.TTData = $scope.TTData;
    //                }
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //};

    //$scope.checkLevel = function (ev, alldata) {
    //    debugger;
    //    if (document.getElementById('PartTerm').checked) {
    //        debugger;
    //        $scope.progpartterm = false;
    //    }
    //    $http({
    //        method: 'POST',
    //        url: 'api/EligibilityCriteriaConfiguration/checkLevel',
    //        data: alldata,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            if (response.response_code == "0") {
    //                $state.go('login');
    //            }
    //            if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                debugger;
    //                $scope.radioChange = response.obj;

    //                if ($scope.radioChange == 0) {
    //                    debugger;
    //                    var confirm = $mdDialog.confirm()
    //                        .title("This is already configured with another level of Configuration.Do you want to continue with the new configuration level?")
    //                        .textContent('')
    //                        .ariaLabel('Lucky Day')
    //                        .targetEvent(ev)
    //                        .ok('Yes')
    //                        .cancel('No');
    //                    $mdDialog.show(confirm).then(function () {
    //                        $scope.getEligibilityCriteria(alldata)
    //                    }, function () {
    //                        debugger;
    //                        $scope.status = 'You decided not to change Configuration level.';
    //                        // $scope.Showbuttonflag1 = false;
    //                        alert($scope.status);
    //                        $state.go('createtimetable');
    //                    });
    //                }
    //                else {
    //                    $scope.getEligibilityCriteria(alldata);
    //                }
    //            }

    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //}

    $scope.getFeeDetails = function (alldata) {
        debugger;
        $localStorage.alldata1 = alldata;
        if (alldata.PRN == null || alldata.PRN == undefined || alldata.AcademicYearId == null || alldata.AcademicYearId == undefined || alldata.ProgrammeId == null || alldata.ProgrammeId == undefined || alldata.ProgPartId == null || alldata.ProgPartId == undefined || alldata.ProgInstPartTermId == null || alldata.ProgInstPartTermId == undefined) {
            alert('Please Select all fields');
        }
        //alldata.configLevel = $localStorage.configLevel;
        //if ($localStorage.configLevel == null || $localStorage.configLevel == undefined) {
        //alldata.configLevel = 1;
        else {
            debugger;
            $http({
                method: 'POST',
                url: 'api/FeeCategoryChangeAfterAdmission/getFeeDetails',
                data: alldata,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        debugger;
                        //$scope.elig = false;
                        $scope.visibility = false;
                        $scope.allFeeCategoryData = response.obj;
                        $scope.oldfeecategory = $scope.allFeeCategoryData[0].OldFeeCategoryName;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.hide = function (isPaperSelectionByStudent) {
        debugger;
        if (isPaperSelectionByStudent == "false") {
            $scope.filter.IsPaperSelectionBeforeFees = false;
            $scope.disable = true;
        }

        if (isPaperSelectionByStudent == "true") {
            $scope.disable = false;
        }
    };


    $scope.sendData = function (filter,data) {
        debugger;
       // $scope.NewFeeCategoryId = data.NewFeeCategoryId;
       
        debugger;
        $http({
            method: 'POST',
            url: 'api/FeeCategoryChangeAfterAdmission/storeNewFeeCategory?NewFeeCategoryId=' + data.NewFeeCategoryId,
            data: filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    debugger;
                    //document.getElementById('vis').style.display = "none";
                    alert('DATA STORED SUCCESSFULLY');
                    $scope.visibility = true;
                    //$scope.elig = true;
                    //$scope.filter = {};
                    //$scope.EligibilityOptions = {};

                    //
                    //$state.go('EligibilityCriteriaConfiguration');
                    //

                    //$scope.buttvisibility = false;
                    //$scope.buttonvisibility = false;
                    //var lengthOfTTData = Object.keys($scope.TTData).length;
                    ////var lengthOfTTData = getObjectLength($scope.TTDataToBePassed);
                    //for (var i = 0; i < lengthOfTTData; i++) {
                    //    var elem = document.getElementById('submit');
                    //    if ($scope.TTData[i].buttonVisibility == 1) {
                    //        elem.style.display = "block";
                    //    }
                    //}

                    //$localStorage.TTData = $scope.TTData;
                    //$scope.TTC = DATA;
                    //$scope.TTC.TTData = $scope.TTData;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
});