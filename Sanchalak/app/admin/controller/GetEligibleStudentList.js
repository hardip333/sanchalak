﻿app.controller('GetEligibleStudentListController', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {
    debugger;
    $scope.filter = {};
    $scope.visibility = false;
    $scope.store = true

    $scope.getInstList = function () {
        debugger;
        $http({
            method: 'POST',
            url: 'api/GetEligibleStudentList/getInstList',
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
                    $scope.InstList = response.obj;
                    if ($localStorage.filter != null && $localStorage.filter != undefined && $localStorage.filter != "") {
                        debugger;
                        //$scope.filter = $localStorage.filter;
                        $scope.getProgList($localStorage.filter.InstituteId);
                    }
                    //if ($localStorage.filter != null && $localStorage.filter != undefined && $localStorage.filter != "")
                    //$scope.filter.InstituteId = $localStorage.filter.InstituteId;
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
    }
    //if ($localStorage.filter != null && $localStorage.filter != undefined && $localStorage.filter != "") {
    //    debugger;
    //    $scope.filter = $localStorage.filter;
    //    //$scope.getInstList();
    //}
    
    //$scope.visibile = true;
   //document.getElementById('vis').style.display = "none";
    $scope.ShowTable = false;
    $rootScope.pageTitle = "Manage Create Table";
    
    //$scope.ShowDate = false;
    $rootScope.showLoading = false;

    //$scope.getInstList = function () {
    //    debugger;
    //    $http({
    //        method: 'POST',
    //        url: 'api/GetEligibleStudentList/getInstList',
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
    //                $scope.InstList = response.obj;
    //                //if ($localStorage.filter != null && $localStorage.filter != undefined && $localStorage.filter != "")
    //               //$scope.filter.InstituteId = $localStorage.filter.InstituteId;
    //                //var lengthOfTTData = Object.keys($scope.TTData).length;
    //                ////var lengthOfTTData = getObjectLength($scope.TTDataToBePassed);
    //                //for (var i = 0; i < lengthOfTTData; i++) {
    //                //    var elem = document.getElementById('submit');
    //                //    if ($scope.TTData[i].buttonVisibility == 1) {
    //                //        elem.style.display = "block";
    //                //    }
    //                //}

    //                //$localStorage.TTData = $scope.TTData;
    //                //$scope.TTC = DATA;
    //                //$scope.TTC.TTData = $scope.TTData;
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });
    //}

    $scope.getProgList = function (FacultyId) {
        
        debugger;
        $http({
            method: 'POST',
            url: 'api/GetEligibleStudentList/getProgList?FacultyId=' + FacultyId,
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
                    $scope.ProgList = response.obj;
                    if ($localStorage.filter != null && $localStorage.filter != undefined && $localStorage.filter != "") {
                        debugger;
                        //$scope.filter = $localStorage.filter;
                        $scope.getProgPartfromProg($localStorage.filter.ProgrammeId);
                    }
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
            url: 'api/GetEligibleStudentList/getProgPartfromProg?ProgId=' + ProgId,
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

    $scope.hide = function () {
       // debugger;
        $scope.store = true;
    }

    $scope.select = function () {
        
        if (document.getElementById('eligibility').checked) {
            checkboxes = document.getElementsByName('admission');
            for (var i = 0, n = checkboxes.length; i < n; i++) {
                checkboxes[i].checked = true;
            }
        }
        else {
            checkboxes = document.getElementsByName('admission');
            for (var i = 0, n = checkboxes.length; i < n; i++) {
                checkboxes[i].checked = false;
            }
        }
        
    }

    $scope.sendData = function (filter) {
        debugger;
        if (filter.AcademicYearId == null || filter.AcademicYearId == undefined || filter.ProgrammeId == null ||
            filter.ProgrammeId == undefined || filter.ProgPartId == null || filter.ProgPartId == undefined) {
            alert('Please select all fields');
        }

        else {
            $localStorage.isEligConsidered = filter.isEligConsidered;
            $localStorage.filter = filter;
            //debugger;
            $http({
                method: 'POST',
                url: 'api/GetEligibleStudentList/getEligibleStudentCountforBranch',
                data: filter,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    debugger;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    if (response.response_code != "200") {
                        debugger;
                        //alert('Oops! Something went wrong.Please Try Again');
                        alert(response.obj);
                        if (response.obj == 'Please Define Eligiblity for this Programme Part First.') {
                            $state.go('EligibilityCriteriaConfiguration');
                        }
                    }
                    
                    else {
                        debugger;
                        $scope.EligibleStudentCountforBranch = response.obj;
                        // $localStorage.EligibleStudentCountforBranch = response.obj;
                        $localStorage.SpecIdList = $scope.EligibleStudentCountforBranch.SpecialisationId;
                        $scope.store = false;
                        //document.getElementById('vis').style.display = "none";
                        //alert('DATA STORED SUCCESSFULLY');
                        //$scope.visibility = true;
                        //$scope.filter = {};
                        //$scope.EligibilityOptions = {};
                        //$state.go('createtimetable');
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
        }
    };

    $scope.sendStudentList = function (filter,EligibleStudentCountforBranch) {
        debugger;
        $scope.EligibleStudentCountforBranch = {};
        $scope.EligibleStudentCountforBranch.objstudentCountParameters = filter;
        $scope.EligibleStudentCountforBranch.studentCount = EligibleStudentCountforBranch;
        //$scope.EligibleStudentCountforBranch.admlevel = admlevel;
        //$scope.EligibleStudentCountforBranch.isEligConsidered = $localStorage.isEligConsidered;
        $http({
            method: 'POST',
            url: 'api/GetEligibleStudentList/sendStudentList',
            data: $scope.EligibleStudentCountforBranch,
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
                   // debugger;
                    $scope.store = true;
                    alert('DATA STORED SUCCESSFULLY');
                    //$scope.EligibleStudentCountforBranch = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getunmappedfeelist = function (SpecId) {
        $localStorage.SpecialisationId = SpecId;
        $state.go('StudentPendingForFeeCategoryMapping');
    }
});


        //if ($localStorage.alldata1.radioValue == "Part") {
        //    debugger;
        //    Status[i] = $localStorage.EligibilityOptions[i].ProgPartId;
        //    while (j < markedCheckbox.length) {
        //        debugger;
        //        if (markedCheckbox[j].checked) {
        //            Status[i].push(markedCheckbox[i].value);
        //            //Status[i][j] = markedCheckbox[j].value;
        //            j++;
        //            if (j == 0) {
        //                if ((j + 1) % 4 == 0) {
        //                    break;
        //                }
        //            }
        //            else if (j % 4 == 0) {
        //                break;
        //            }
        //            else {
        //                continue;
        //            }
        //        }
        //    }
        //}
        //else if (alldata1.radioValue == "PartTerm") {
        //    debugger;
        //    Status[i] = $localStorage.EligibilityOptions[i].ProgPartTermId;
        //    while (j < markedCheckbox.length) {
        //        debugger;
        //        if (markedCheckbox[j].checked) {
        //            //markedCheckbox[j].value = 1;
        //            Status[i][j] = markedCheckbox[j].value;
        //            j++;
        //            if (j == 0) {
        //                if ((j + 1) % 4 == 0) {
        //                    break;
        //                }
        //            }
        //            else if (j % 4 == 0) {
        //                break;
        //            }
        //            else {
        //                continue;
        //            }
        //        }
        //    }
        //}
        //else {
        //    alert('Select a level of configuration');
        //    $state.go('createtimetable');
        //}

        //for (var i = 0; i < Status.length; i++) {
        //    //debugger;
        //    Status[i] = new Array();
        //}
        //Loop and push the checked CheckBox value in Array.


    ////debugger;
    //$scope.eligibilityData.requiredStatus = Status;
    ////Display the selected CheckBox values.
    //if (Status.length > 0) {
    //    alert("Selected values: " + Status.join(","));
    //    StatusList = Status.join(",");
    //}
    //alert($("#partName option:selected").text());
    //$scope.visibility = true;
    //$http({
    //    method: 'POST',
    //    url: 'api/EligibilityCriteriaConfiguration/saveEligibilityCriteria',
    //    data: alldata,
    //    headers: { "Content-Type": 'application/json' }
    //})
    //    .success(function (response) {
    //        if (response.response_code == "0") {
    //            $state.go('login');
    //        }
    //        else if (response.response_code != "200") {
    //            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //        }
    //        else {
    //            //debugger;
    //            $scope.EligibilityOptions = response.obj;
    //            document.getElementById('vis').style.display = "none";
    //            document.getElementById('vis1').style.display = "block";
    //        }
    //    })
    //    .error(function (res) {
    //        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //    });

