app.controller('VenueReAllocationCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Venue Re-allocation";

    $rootScope.showLoading = false;
    $scope.checkDataExists = false;
    $scope.showReVenueSectionFlag = false;

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.cancelSeatNoGeneration = function () {
        $scope.examseat = {
     
            ProgrammeId:0,
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammePartTermId: 0,
            BranchId:0
        };
    };
    $scope.cancelSeatNoGeneration();

    /*Venue Re-Allocation List*/
    $scope.VenueReAllocationVenueGetByProgPTId = function () {

        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/VenueReAllocation/VenueReAllocationVenueGetByProgPTId',
            data: $scope.examseat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.VenueListTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.VenueList = response.obj;
                   
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
       
    };

    /*Get Student List for Re-allocation*/
    $scope.VenueReAllocationStudentsGet = function () {

        $scope.showReVenueSectionFlag = false;
        $scope.checkDataExists = false;
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/VenueReAllocation/VenueReAllocationStudentsGet',
            data: $scope.examseat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                    $scope.offSpinner();

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                }
                else {

                    $scope.StudentReallocationTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.StudentReallocationData = response.obj;
                    $scope.offSpinner();
                   
                    if (response.obj.length == 0) {
                        $scope.checkDataExists = true;
                    }
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
        
    };

    /*Venue Re-Allocation Venue List*/
    $scope.VenueReAllocationVenueGet = function () {

        
        $rootScope.showLoading = true;
        $scope.showReVenueSectionFlag = true;
        $http({
            method: 'POST',
            url: 'api/VenueReAllocation/VenueReAllocationVenueGet',
            data: $scope.examseat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.ReVenueListTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.ReVenueList = response.obj;
                    if (response.obj.length == 0) {
                        $scope.showReVenueSectionFlag = false;
                    }
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
     
    };

    /*Venue Re-Allocation for Students*/
    $scope.VenueReAllocationforStudents = function () {

        $scope.BulkPRNforReAllocation = "";
        $scope.SelectedCheckCount = 0;
        for (var i = 0; i < $scope.StudentReallocationData.length; i++) {
            if ($scope.StudentReallocationData[i].CheckVenueSelected) {
                var PRN = $scope.StudentReallocationData[i].PRN;
                $scope.BulkPRNforReAllocation += + PRN + ",";
                $scope.SelectedCheckCount++;
            }
        }
       

        $scope.BulkPRNforReAllocation = $scope.BulkPRNforReAllocation.slice(0, -1);
        $scope.ReAllocationPRNData = $scope.BulkPRNforReAllocation;

        $scope.FinalReAllocationObj = {};
        $scope.FinalReAllocationObj.SelectedCheckCount = $scope.SelectedCheckCount;
        $scope.FinalReAllocationObj.ReAllocationPRNData = $scope.ReAllocationPRNData;
        $scope.FinalReAllocationObj.ExamReVenueId = $scope.examseat.ExamReVenueId;
        $scope.FinalReAllocationObj.ExamVenueId = $scope.examseat.ExamVenueId;
        $scope.FinalReAllocationObj.ProgrammePartTermId = $scope.examseat.ProgrammePartTermId;

        if ($scope.FinalReAllocationObj.SelectedCheckCount == 0 || $scope.FinalReAllocationObj.SelectedCheckCount == null || $scope.FinalReAllocationObj.SelectedCheckCount == "" || $scope.FinalReAllocationObj.SelectedCheckCount == undefined) {
            alert("Please select at least one student for Venue Re-Allocation..!");
        }
        else if ($scope.examseat.ExamReVenueId == null || $scope.examseat.ExamReVenueId == "" || $scope.examseat.ExamReVenueId == undefined) {
            alert("Please select Exam Venue for Re-Allocation..!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/VenueReAllocation/VenueReAllocationEditforStudents',
                data: $scope.FinalReAllocationObj,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.VenueReAllocationStudentsGet();
                        $scope.FinalReAllocationObj = {};
                        $scope.showReVenueSectionFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
       
    };

    // This executes when checkbox in table header is checked
    $scope.selectAll = function () {
        // Loop through all the entities and set their isChecked property
        for (var i = 0; i < $scope.StudentReallocationData.length; i++) {
            $scope.StudentReallocationData[i].CheckVenueSelected = $scope.examseat.CheckVenueSelected;
        }
    }
});