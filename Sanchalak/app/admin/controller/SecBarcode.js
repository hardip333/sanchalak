﻿app.controller('SecBarcodeCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, Upload) {
   
    var favoriteCookie = $cookies.get('token');    
    if (!favoriteCookie) {
        $state.go('login');
    }      
 
    $scope.SecBarcodeGet = function () {
        
        $http({
            method: 'POST',
            url: 'api/SecBarcode/SecBarcodeGet',
            data: $scope.BarcData,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                              
                if (response.response_code == "0") {

                    alert(response.obj);
                    $state.go('login');
                }
                else if (response.response_code == "201") {

                    alert(response.obj);
                }
                else {

                    //window.open(response.obj, '_blank');

                    $scope.BarcData = response.obj;                    
                    //$scope.BarcData = {};
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Spinner ON
    $scope.onSpinner = function on() {

        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $rootScope.showLoading = false;

    $scope.cancelBlankMarksheet = function () {
        $scope.filter = {


            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammeId: 0,
            BranchId: 0,
            ProgrammePartTermId: 0,
            TeachingLearningMethodId: 0,
            AssessmentMethodId: 0
        };
    };

    $scope.cancelBlankMarksheet();

    $scope.getMstTeachingLearningMethodGetForDropDownByPartTermId = function () {
        var xml = new Object();

        xml.PartTermId = $scope.filter.ProgrammePartTermId;
        xml.SpecialisationId = $scope.filter.BranchId;
        $http({
            method: 'POST',
            url: 'api/MstTeachingLearningMethod/TeachingLearningGetForDropDownByPartTermId',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {

                    $scope.defaultTeachingList = response.obj;
                    //$scope.MstTeachingLearningMethodGetForDropDownList = $scope.defaultTeachingList.concat($scope.tempList);

                }
            })
            .error(function (res) {
                alert(res.obj);
            });
    };

    $scope.getAssessmentForDropDownByPartTermId = function () {

        var xml = new Object();

        xml.PartTermId = $scope.filter.ProgrammePartTermId;
        xml.SpecialisationId = $scope.filter.BranchId;

        $http({
            method: 'POST',
            url: 'api/MstAssessmentMethod/AssessmentGetForDropDownByPartTermId',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {

                    $scope.defaultAssessmentList = response.obj;
                    //$scope.MstTeachingLearningMethodGetForDropDownList = $scope.defaultTeachingList.concat($scope.tempList);

                }
            })
            .error(function (res) {
                alert(res.obj);
            });
    };

    // for list in table
    $scope.getPaperListForSecBarcode = function () {

        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/SecBarcode/getPaperListForSecBarcode',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {

                    $scope.PaperListForBlankMarksheetList = response.obj;

                    $scope.details = response.obj.PaperList;


                    $scope.marksheetTableParams = new NgTableParams({
                        // count: 1000
                    }, {
                        dataset: $scope.details
                    });


                }


            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.downloadSecBarcode = function (data) {
        //debugger
        var xml = new Object();

        xml.MstPaperId = data.Id;
        xml.ExamMasterId = $scope.filter.ExamMasterId;
        xml.ProgrammePartTermId = $scope.filter.ProgrammePartTermId;
        xml.BranchId = $scope.filter.BranchId;
        xml.TeachingLearningMethodId = $scope.filter.TeachingLearningMethodId;
        xml.AssessmentMethodId = $scope.filter.AssessmentMethodId;
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/SecBarcode/generateSecBarcode',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                    $scope.offSpinner();
                }
                else {
                    if (response.obj == "No Record Found") {
                        alert("No students in this Paper !");
                        $scope.offSpinner();
                    }
                    else {
                        alert(response.obj);
                        $scope.offSpinner();
                        //window.location = response.obj;
                        window.open(response.obj, '_blank');
                        //location.href("http://172.25.15.22/MSUIS_AdminAPI/Upload/BlankMarksheet/BlankMarksheetForCommunication%20Skills%20in%20English.pdf");
                        // $scope.getPaperListForBlankMarksheet();
                    }
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
                $scope.offSpinner();
            });
    };
});



