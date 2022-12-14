app.controller('StudentPendingForFeeCategoryMappingController', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    //debugger;
    $scope.ShowTable = false;
    $rootScope.pageTitle = "Map Pending Fee Category";
    $rootScope.showLoading = false;
    $scope.feeAssignFlag = false;

    $scope.getSpecId = function () {
        /* alert('in');*/
        //debugger;
        /*var r = $scope.Student.date;*/
        //let urlstring = window.location.href;
        //let paramString = urlstring.split('?')[1];
        //let param_arr = paramString.split('&');
        /*let queryString = new URLSearchParams(paramString);*/
        /*const param = [];*/
        //for (let pair of queryString.entries()) {
        //    var prn1 = pair[1];
        //    var d1 = pair[3];

        //    //param[0] = pair[1];
        //    //param[1] = pair[3];
        //    //console.log("Key is: " + pair[0]);
        //    //console.log("Value is: " + pair[1]);
        //    /*console.log(r);*/
        //}
        //return [prn1 , d1];
        //let pair1 = [];
        //pair1 = paramString.split('=');
        //var SpecId = pair1[1];
        //pair2 = param_arr[1].split('=');
        //var d1 = pair2[1];
        //for (let i = 0; i < param_arr.length; ++i) {
        //    pair = param_arr[i].split('=');
        //    /*console.log(pair[0]);*/
        //    /*var prn1 = pair[1];*/
        //    //var d1 = pair[3];
        //}
        //var prn1 = pair[1];
        //var d1 = pair[3];
        $scope.getUnmappedFeeCatStudentList($localStorage.SpecialisationId);
        //return SpecId;
    }

    $scope.getUnmappedFeeCatStudentList = function (SpecId) {
        debugger;
        //$scope.filter.SpecId = SpecId;
        $http({
            method: 'POST', 
            url: 'api/StudentPendingForFeeCategoryMapping/getUnmappedFeeCatStudentList?SpecId=' + SpecId,
            data: $localStorage.filter,
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
                    //debugger;
                    $scope.studentList = response.obj;
                    //$scope.feeCategoryList = response.obj;
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

    $scope.sendNewFeeCategoryList = function (studentList) {
        debugger;
       // $scope.hide = true;
        //$scope.studentListLength = studentList.length;
        //studentList.totalLength = studentList.length
        //for (var i = 0; i < studentList.totalLength; i++) {
        //    debugger;
        //    if (studentList[i].oldAdmissionFeeCategoryId != null && studentList[i].oldAdmissionFeeCategoryId != undefined &&
        //        studentList[i].oldAdmissionFeeCategoryId != "" && studentList[i].oldAdmissionFeeCategoryId != 0) {
        //        $scope.feeAssignFlag = true;
        //        break;
        //    }
        //    else {
        //        continue;
        //    }
        //}
        //if ($scope.feeAssignFlag == false) {
        //    alert('Please select New Fee Category for atleast one student');
        //}

        //alert('DATA STORED SUCCESSFULLY');
        //$state.go('GetEligibleStudentList');

        //debugger;
        //$scope.studentListWithNewMappedFeeCategory = [];
        //$scope.studentListWithNewMappedFeeCategory.PRN;
        //$scope.studentListWithNewMappedFeeCategory.FeeCategoryId;
        //$scope.studentListWithNewMappedFeeCategory.ProgrammeInstancePartTermId;


        //for (var i = 0; i < studentList.length; i++){
        //    debugger;
        //    $scope.studentListWithNewMappedFeeCategory[i].PRN = studentList[i].PRN;
        //    $scope.studentListWithNewMappedFeeCategory[i].FeeCategoryId = studentList[i].FId;
        //    $scope.studentListWithNewMappedFeeCategory[i].ProgrammeInstancePartTermId = studentList[0].ApplicableFeeCategories[0].PRN;
        //}       

        /*else {*/
            $scope.studentList = studentList;

			$http({
				method: 'POST',
				url: 'api/StudentPendingForFeeCategoryMapping/sendNewFeeCategoryList',
				data: $scope.studentList,
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
                        alert('Fee Category Updated Successfully');
                        $state.go('GetEligibleStudentList');
						//$state.go('StudentPendingForFeeCategoryMapping');



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

    $scope.goBack = function () {
        debugger;
        $scope.filter = $localStorage.filter;
        $state.go('GetEligibleStudentList');
    }
    

    $scope.getProgPartfromProg = function (ProgId) {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/EligibilityCriteriaConfiguration/getProgPartfromProg?ProgId=' + ProgId,
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
                    //debugger;
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

    $scope.getProgPartTermfromProgPart = function (ProgPartId) {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/EligibilityCriteriaConfiguration/getProgPartTermfromProgPart?ProgPartId=' + ProgPartId,
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
                    //debugger;
                    $scope.ProgPartTermList = response.obj;
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

    $scope.checkLevel = function (ev, alldata) {
        //debugger;
        if (document.getElementById('PartTerm').checked)
            $scope.progpartterm = false;
        $http({
            method: 'POST',
            url: 'api/EligibilityCriteriaConfiguration/checkLevel',
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
                    //debugger;
                    $scope.radioChange = response.obj;
                    if ($scope.radioChange == 0) {
                        var confirm = $mdDialog.confirm()
                            .title("This is already configured with another level of Configuration.Do you want to continue with the new configuration level?")
                            .textContent('')
                            .ariaLabel('Lucky Day')
                            .targetEvent(ev)
                            .ok('Yes')
                            .cancel('No');
                        $mdDialog.show(confirm).then(function () {
                            $scope.getEligibilityCriteria(alldata)
                        }, function () {
                            $scope.status = 'You decided not to change Configuration level.';
                            // $scope.Showbuttonflag1 = false;
                            alert($scope.status);
                            $state.go('createtimetable');
                        });
                    }
                    else {
                        $scope.getEligibilityCriteria(alldata);
                    }
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.getEligibilityCriteria = function (alldata) {
        $scope.elig = false;
       // debugger;
        $localStorage.alldata1 = alldata;

        //alldata.configLevel = $localStorage.configLevel;
        //if ($localStorage.configLevel == null || $localStorage.configLevel == undefined) {
        //alldata.configLevel = 1;
        $http({
            method: 'POST',
            url: 'api/EligibilityCriteriaConfiguration/getEligibilityCriteria',
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
                    //debugger;
                    $scope.EligibilityOptions = response.obj;
                    $scope.MainName = $scope.EligibilityOptions[0].ParentName;
                    $localStorage.length = response.obj.length;
                    document.getElementById('vis').style.display = "block";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };


    $scope.sendData = function (filter, EligibilityOptions) {
        //debugger;
        $scope.dataSending = {};
        $scope.dataSending.forDatabase = filter;
        $scope.dataSending.parentData = EligibilityOptions;
        //let j = 0;
        //var markedCheckbox = document.getElementsByName('eligi');
        //for (var i = 0; i < $localStorage.length; i++) {
        //    debugger;
        //    if ($localStorage.alldata1.radioValue == "Part") {
        //        $scope.ProgPartIdforDb = EligibilityOptions[i].ProgPartId;
        //        $scope.PreviousName = EligibilityOptions[i].PreviousNames;
        //    }
        //    else if ($localStorage.alldata1.radioValue == "PartTerm") {
        //        $scope.ProgPartTermIdforDb = EligibilityOptions[i].ProgPartTermId;
        //        $scope.PreviousName = EligibilityOptions[i].PreviousNames;
        //    }
        //    else {
        //        alert('Select a level of configuration');
        //        $state.go('createtimetable');
        //    }
        //    while (j < markedCheckbox.length) {
        //        debugger;
        //        if (markedCheckbox[j].checked) {
        //            debugger;
        //            if (markedCheckbox[j].value == "PASS") {
        //                $scope.eligibilityLabel = "PASS";
        //                $scope.EStatus = true;
        //            }
        //            if (markedCheckbox[j].value == "FAIL/ATKT") {
        //                if ($scope.eligibilityLabel == "PASS") {
        //                    $scope.checkBoth = 1;
        //                }
        //                else {
        //                    $scope.checkBoth = 0;
        //                }
        //                //$scope.eligibilityLabel = "FAIL/ATKT";
        //                //$scope.Status = true;
        //            }
        //            if (markedCheckbox[j].value == "FAIL") {
        //                $scope.eligibilityLabel = "FAIL";
        //                $scope.EStatus = true;
        //            }
        //            if (markedCheckbox[j].value == "ABSENT") {
        //                $scope.eligibilityLabel = "ABSENT";
        //                $scope.EStatus = true;
        //            }
        //            j++;
        //            if (j % 4 == 0) {
        //                break;
        //            }
        //        }
        //        else {
        //            j++;
        //            if (j % 4 == 0) {
        //                break;
        //            }
        //        }
        //    }
        //    $scope.dataSending.dataToBeSent.push({ PreviousName: $scope.PreviousName, ProgPartIdforDb: $scope.ProgPartIdforDb, ProgPartTermIdforDb: $scope.ProgPartTermIdforDb, eligibilityLabel: $scope.eligibilityLabel, EStatus: $scope.EStatus, checkBoth: $scope.checkBoth });


        //debugger;
        $http({
            method: 'POST',
            url: 'api/EligibilityCriteriaConfiguration/storeEligibilityCriteria',
            data: $scope.dataSending,
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
                    document.getElementById('vis').style.display = "none";
                    alert('DATA STORED SUCCESSFULLY');
                    $scope.visibility = true;
                    $scope.elig = true;
                    //$scope.filter = {};
                    //$scope.EligibilityOptions = {};
                    $state.go('createtimetable');
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

        