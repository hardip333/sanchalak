app.controller('AssignBlocksCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Assign Blocks";
    $scope.FlagAssignSection = false;

    $rootScope.showLoading = false;
    $scope.isPaperWiseAllocationChecked = false;

    $scope.showButtonFlag = true;
    $scope.showManualAllocationFlag = false;
    $scope.showManualList = function () {
        $scope.showButtonFlag = true;
        $scope.showManualAllocationFlag = true;

        $scope.getavailablegetAvailableExamBlockList();
    }

    $scope.backToButtonFlag = function () {
        $scope.showButtonFlag = true;
        $scope.showManualAllocationFlag = false;
    }

    //$scope.cancelMstTimeTableMasterAdd = function () {
    //    $scope.filter = {

    //        ProgrammeId:0,
    //        ExamMasterId: 0,
    //        FacultyExamMapId: 0,
    //        ProgrammePartTermId: 0,
    //    /*    BranchId:0*/
    //    };
    //};

    //$scope.cancelMstTimeTableMasterAdd();

    // for get specilization list
    $scope.pendingMstSpecialisationGetByPId = function () {

        $rootScope.showLoading = true;
        $scope.FlagAssignSection = true;

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

      

        //var len = $scope.MstSpecialisationGetByPIdList.length;

        //$scope.specialisationIds = "";
        //// for  comma separated string
        //for (var i = 0; i < len; i++) {

        //    if ($scope.MstSpecialisationGetByPIdList[i].IsSelected)
        //        $scope.specialisationIds += $scope.MstSpecialisationGetByPIdList[i].Id + ",";

        //}

        
        // for last comma remove from string 
        if ($scope.filter.ExamCenter == null || $scope.filter.ExamCenter == undefined || $scope.filter.ExamCenter == "") {
            alert("Please select Exam Center..!!");
        }

        else if ($scope.filter.Specialisation == null || $scope.filter.Specialisation == undefined || $scope.filter.Specialisation == "") {
            alert("Please select Specialisation..!!");
        }
        else {
            $scope.isPaperWiseAllocationChecked = true;
            $scope.filter.ExamVenueExamCenterId = $scope.filter.ExamCenter.ExamVenueExamCenterId;
            $scope.filter.CenterName = $scope.filter.ExamCenter.CenterName;
            $scope.filter.SpecialisationId = $scope.filter.Specialisation.Id;

            $rootScope.showLoading = true;

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

                        console.log("====");
                        console.log($scope.MstPaperListGetByProgPartTermIdAndSpeciliationIdList);
                    }

                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });
        }


    }

    // for auto allocation

    $scope.showAutoAllocationBlockList = function () {


        var len = $scope.paperList.length;

        $scope.paperIds = "";
        // for  comma separated string for paper id
        for (var i = 0; i < len; i++) {

            if ($scope.paperList[i].PaperChecked)

                $scope.paperIds += $scope.paperList[i].Id + ",";

        }
        // for last comma remove from string 
        if ($scope.paperIds.length > 0) {

            $scope.paperIds = $scope.paperIds.substring(0, $scope.paperIds.length - 1);


            $scope.filter.PaperId = $scope.paperIds;

            $scope.filter.SpecialisationId = $scope.filter.Specialisation.Id;
            $scope.filter.ExamVenueExamCenterId = $scope.filter.ExamCenter.ExamVenueExamCenterId;
            $rootScope.showLoading = true;


            $http({
                method: 'POST',
                url: 'api/BlockAllocation/AssignBlocks',
                data: $scope.filter,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        alert(response.obj);
                    }
                    else {
                        $scope.AssignBlocksList = response.obj;
                        alert($scope.AssignBlocksList);
                        $scope.FlagAssignSection = false;
                        $scope.isPaperWiseAllocationChecked = false;
                        $scope.showManualAllocationFlag = false;
                        $scope.filter = {};

                    }


                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });

        }
        else {

            alert("Select atleast one Paper");

        }
    };


    // for manuall allocation
    $scope.getavailablegetAvailableExamBlockList = function () {


        var len = $scope.paperList.length;

        $scope.paperIds = "";
        // for  comma separated string for paper id
        for (var i = 0; i < len; i++) {

            if ($scope.paperList[i].PaperChecked)

                $scope.paperIds += $scope.paperList[i].Id + ",";

        }
        // for last comma remove from string 
        if ($scope.paperIds.length > 0) {

            $scope.paperIds = $scope.paperIds.substring(0, $scope.paperIds.length - 1);


            $scope.filter.PaperId = $scope.paperIds;


            $scope.filter.SpecialisationId = $scope.filter.Specialisation.Id;


            $rootScope.showLoading = true;


            $http({
                method: 'POST',
                url: 'api/BlockAllocation/getAvailableExamBlockList',
                data: $scope.filter,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        alert(response.obj);
                    }
                    else {


                        $scope.availableExamBlockList = response.obj;
                        $scope.manualExamBlockList = response.obj.ExamBlockList;

                        /*            var data = $scope.manualExamBlockList.slice();*/

                        $scope.manualTableParams = new NgTableParams({
                            count: 1000
                        }, {
                            dataset: $scope.manualExamBlockList
                        });
                    }


                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });

        }
        else {

            alert("Select atleast one Paper");

        }
    };


    // for process manuall allocation
    $scope.processManualAssignBlocks = function () {


        var len = $scope.manualExamBlockList.length;

        $scope.blockIds = "";
        // for  comma separated string for paper id
        for (var i = 0; i < len; i++) {

            if ($scope.manualExamBlockList[i].IsSelected)

                $scope.blockIds += $scope.manualExamBlockList[i].Id + ",";

        }
        
        // for last comma remove from string 
        if ($scope.blockIds.length > 0) {

            $scope.blockIds = $scope.blockIds.substring(0, $scope.blockIds.length - 1);

            $scope.filter.ExamBlockId = $scope.blockIds;

            $scope.filter.PaperId = $scope.paperIds;

            $scope.filter.SpecialisationId = $scope.filter.Specialisation.Id;
            $scope.filter.ExamVenueExamCenterId = $scope.filter.ExamCenter.ExamVenueExamCenterId;
            $scope.filter.CenterName = $scope.filter.ExamCenter.CenterName;

            $rootScope.showLoading = true;
            $http({
                method: 'POST',
                url: 'api/BlockAllocation/ManualAssignBlocks',
                data: $scope.filter,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        alert(response.obj);
                    }
                    else {
                        $scope.processManualAssignBlocksList = response.obj;

                        alert(response.obj);
                        $scope.FlagAssignSection = false;
                        $scope.isPaperWiseAllocationChecked = false;
                        $scope.showManualAllocationFlag = false;
                        $scope.filter = {};
                    }

                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });

        }
        else {

            alert("Select atleast one exam block");

        }
    };



    //Start - Mohini's Code 23-Apr-2022============================

    /*Get ExamVenueGetforAssignBlocks List*/
    $scope.ExamVenueGetforAssignBlocks = function () {

        $scope.FlagAssignSection = true;

        $http({
            method: 'POST',
            url: 'api/BlockAllocation/ExamVenueGetforAssignBlocks',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamCenterTableParams = new NgTableParams({}, { dataset: response.obj });
                $scope.ExamCenterList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });

    };


    $scope.selectAllPaper = function () {

        // Loop through all the entities and set their isChecked property
        for (var i = 0; i < $scope.MstPaperListGetByProgPartTermIdAndSpeciliationIdList.PaperList.length; i++) {
            $scope.MstPaperListGetByProgPartTermIdAndSpeciliationIdList.PaperList[i].PaperChecked = $scope.paper.PaperChecked;
        }
    }

    //End - Mohini's Code 23-Apr-2022============================

});