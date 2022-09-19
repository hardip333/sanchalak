app.controller('ExamFormBarcodeGenerationCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage ExamForm Barcode Generation";


    $rootScope.showLoading = false;
    $scope.isPaperWiseAllocationChecked = false;

    // for get specilization list
    $scope.pendingMstSpecialisationGetByPId = function () {

        $rootScope.showLoading = true;


        $http({
            method: 'POST',
/*            url: 'api/TimeTableMaster/MstTimeTableMasterGetPending',*/
            url: 'api/BlockAllocation/MstSpecialisationGetByPId',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {

                    $scope.MstSpecialisationGetByPIdList = response.obj.SpecialisationsList;
                    $scope.details = response.obj;

                }
             
    
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };


     // for get paper list
    $scope.getPaperList = function () {

        $scope.isPaperWiseAllocationChecked = true;

        var len = $scope.MstSpecialisationGetByPIdList.length;

        $scope.specialisationIds = "";
        // for  comma separated string
        for (var i = 0; i < len; i++) {

            if ($scope.MstSpecialisationGetByPIdList[i].IsSelected)
                $scope.specialisationIds += $scope.MstSpecialisationGetByPIdList[i].Id + ",";

        }
        // for last comma remove from string 
        if ($scope.specialisationIds.length > 0) {

            $scope.specialisationIds = $scope.specialisationIds.substring(0, $scope.specialisationIds.length - 1);

            $rootScope.showLoading = true;

            $scope.filter.SpecialisationId = $scope.specialisationIds;

            $http({
                method: 'POST',
                url: 'api/BlockAllocation/MstPaperListGetByProgPartTermIdAndSpeciliationId',
                data: $scope.filter,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        alert(response.obj);
                    }
                    else {

                        $scope.MstPaperListGetByProgPartTermIdAndSpeciliationIdList = response.obj;
                        $scope.paperList = response.obj.PaperList;
            
                  
                     

                    }

                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });
        }
        else {

            alert("Please Select atleast one Specialisation");

        }


    }


// for barcode generation 

    $scope.saveExamFormBarcodePdfGenerationRequest = function () {
    

        var len = $scope.paperList.length;

        $scope.StrPaperIds = "";
        // for  comma separated string for paper id
        for (var i = 0; i < len; i++) {

            if ($scope.paperList[i].PaperChecked)

                $scope.StrPaperIds += $scope.paperList[i].Id + ",";

        }
        // for last comma remove from string 
        if ($scope.StrPaperIds.length > 0) {

            $scope.StrPaperIds = $scope.StrPaperIds.substring(0, $scope.StrPaperIds.length - 1);

       

            $scope.filter.SpecialisationIds = $scope.specialisationIds;
            $scope.filter.PaperIds = $scope.StrPaperIds;
         

            $rootScope.showLoading = true;

            $http({
                method: 'POST',
                url: 'api/BlockAllocation/saveExamFormBarcodePdfGenerationRequest',
                data: $scope.filter,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        //$scope.cancelMstExamCenterAdd();
                        //$scope.getMstExamCenterGet();
                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });

        

        }
        else {

            alert("Please Select atleast one Paper");

        }
    };





    // for barcode generation 

    //$scope.generateExamformBarcode = function () {
    //    alert(1000);

    //    var len = $scope.paperList.length;

    //    $scope.StrPaperIds = "";
    //    // for  comma separated string for paper id
    //    for (var i = 0; i < len; i++) {

    //        if ($scope.paperList[i].PaperChecked)

    //            $scope.StrPaperIds += $scope.paperList[i].Id + ",";

    //    }
    //    // for last comma remove from string 
    //    if ($scope.StrPaperIds.length > 0) {

    //        $scope.StrPaperIds = $scope.StrPaperIds.substring(0, $scope.StrPaperIds.length - 1);


    //        $scope.filter.strPaperId = $scope.StrPaperIds;

    //        $scope.filter.SpecialisationId = $scope.specialisationIds;

    //        $rootScope.showLoading = true;


    //        $http({
    //            method: 'POST',
    //            url: 'api/BlockAllocation/generateExamFormBarcode',
    //            data: $scope.filter,
    //            headers: { "Content-Type": 'application/json' }
    //        })
    //            .success(function (response) {
    //                $rootScope.showLoading = false;

    //                if (response.response_code != "200") {
    //                    alert(response.obj);
    //                }
    //                else {
    //                    $scope.generateExamFormBarcodeList = response.obj;
    //                    alert(response.obj);


    //                }


    //            })
    //            .error(function (res) {
    //                $rootScope.showLoading = false;
    //                alert(res.obj);
    //            });

    //    }
    //    else {

    //        alert("Select atleast one Paper");

    //    }
    //};


    // ================= Start - Mohini's Code on 16-Apr-2022 ========================

    $scope.selectAllPaper = function () {

        // Loop through all the entities and set their isChecked property
        for (var i = 0; i < $scope.MstPaperListGetByProgPartTermIdAndSpeciliationIdList.PaperList.length; i++) {
            $scope.MstPaperListGetByProgPartTermIdAndSpeciliationIdList.PaperList[i].PaperChecked = $scope.paper.PaperChecked;
        }
    }
    // ================= End - Mohini's Code on 16-Apr-2022 ========================
});