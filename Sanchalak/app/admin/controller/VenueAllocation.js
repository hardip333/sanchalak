﻿app.controller('VenueAllocationCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Seat Number Generation";
    $scope.showVenueAllocationFlag = false;
    $scope.VenueListFlag = false;

    $rootScope.showLoading = false;

    //Students count for based on Seat number and Venue selection
    $scope.AllocateVenueCount = function () {

        $rootScope.showLoading = true;
        $scope.showVenueAllocationFlag = false;
        $http({ 
            method: 'POST',
            url: 'api/VenueAllocation/VenueAllocationDetailsCount',
            data: $scope.examseat,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.DetailsCount = response.obj;
                    $scope.ModelDetailsCount = {};
                    $scope.ModelDetailsCount.SeatNoGeneratedCount = $scope.DetailsCount.SeatNoGeneratedCount;
                    $scope.ModelDetailsCount.VenueAllocatedCount = $scope.DetailsCount.VenueAllocatedCount;
                    $scope.ModelDetailsCount.VenueAllocatedPendingCount = $scope.DetailsCount.VenueAllocatedPendingCount;
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    //Venue Allocation List
    $scope.VenueAllocationVenueGet = function () {

        if ($scope.examseat.ProgrammePartTermId == null || $scope.examseat.ProgrammePartTermId == undefined || $scope.examseat.ProgrammePartTermId == '') {
            alert("Please select Programme..!");
        }
        else if ($scope.ModelDetailsCount.SeatNoGeneratedCount == $scope.ModelDetailsCount.VenueAllocatedCount) {
            alert("Venue has been already allocated to all students.");
        }
        else {
            $rootScope.showLoading = true;
            $scope.showVenueAllocationFlag = true;
            $http({
                method: 'POST',
                url: 'api/VenueAllocation/VenueAllocationVenueGet',
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
                        if (response.obj.length == 0) {
                            $scope.VenueListFlag = true;
                        }
                    }
                })
                .error(function (res) {

                    $rootScope.showLoading = false;
                    alert(res.obj);
                });
        }
        
    };

    //Go for Venue Allocation of Pending Students
    $scope.VenueAllocationForPendingStudents = function () {

        $scope.TotalAvailableCapacity = 0;
        for (var i = 0; i < $scope.VenueListTableParams.data.length; i++)
        {
            $scope.TotalAvailableCapacity = parseInt($scope.VenueListTableParams.data[i].PendingCapacity) + parseInt($scope.TotalAvailableCapacity);
        }

        for (var i in $scope.VenueListTableParams.data)
        {
            $scope.VenueListTableParams.data[i].ExamMasterId = $scope.examseat.ExamMasterId;
            $scope.VenueListTableParams.data[i].SpecialisationId = $scope.examseat.BranchId;
            $scope.VenueListTableParams.data[i].ProgrammePartTermId = $scope.examseat.ProgrammePartTermId;
        }

        if ($scope.ModelDetailsCount.VenueAllocatedPendingCount > $scope.TotalAvailableCapacity) {
            alert("Pendinng students are more than Total Pending Capacity...!");
        }
        else {
            $http({
                method: 'POST',
                url: 'api/VenueAllocation/VenueAllocationForPendingStudents',
                data: $scope.VenueListTableParams.data,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.showVenueAllocationFlag = false;
                        $scope.VenueListFlag = false;
                        //$scope.examseat = {};
                        $scope.ModelDetailsCount = {};
                        //$scope.cancelSeatNoGeneration();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    $rootScope.showLoading = false;
                    alert(res.obj);
                    $scope.showVenueAllocationFlag = false;
                });
        }
    };

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

});