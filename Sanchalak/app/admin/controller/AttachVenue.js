app.controller('AttachVenueCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Attach Venue";

    $rootScope.showLoading = false;

    $scope.cancelAttachVenue = function () {
        $scope.filter = {
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammeId: 0,
            CourseScheduleMapId: 0,
            ExamCenterId1: 0,
            ExamCenterId2: 0
       
        };

        $scope.pendingvenue = {
            ExamCenterId1: 0,
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammeId: 0
        };


        $scope.attachvenue = {
            ExamCenterId2: 0,
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammeId: 0
        };
    };
    $scope.cancelAttachVenue();

    // for left  table
    $scope.getPendingCourseAttachVenueget = function () {

        $rootScope.showLoading = true;

        if ($scope.filter.ExamCenterId1 > 0) {
            $scope.filter.ExamCenterId = $scope.filter.ExamCenterId1;
         
        } else {
  
            $scope.filter.ExamCenterId = 0;
        }
  

        $http({
            method: 'POST',
            url: 'api/CourseAttachVenue/getPendingCourseAttachVenue',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.pendingCourseAttachVenueList = response.obj;
               
                    $scope.pendingvenueTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: $scope.pendingCourseAttachVenueList
                    });
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
 /*   $scope.getPendingExamFeeConfigurationList();*/

      // for right  table
    $scope.getAttachedCourseVenueMapListget = function () {

        $rootScope.showLoading = true;


        if ($scope.filter.ExamCenterId2 > 0) {
            $scope.filter.ExamCenterId = $scope.filter.ExamCenterId2;
        } else {
            $scope.filter.ExamCenterId = 0;
        }
      

        $http({
            method: 'POST',
            url: 'api/CourseAttachVenue/getAttachedCourseVenueMapList',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.AttachedCourseVenueMapList = response.obj;
       

                    $scope.attachvenueTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: $scope.AttachedCourseVenueMapList
                    });

                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    /*   $scope.getPendingExamFeeConfigurationList();*/

    $scope.moveToAttach = function () {
  
        var len = $scope.pendingCourseAttachVenueList.length;
   
        if ($scope.AttachedCourseVenueMapList === undefined || $scope.AttachedCourseVenueMapList === null )
            $scope.AttachedCourseVenueMapList = [];

        for (var i = 0; i < len; i++) {
        
            if ($scope.pendingCourseAttachVenueList[i].ToBeAttached) {
           
                var temp = $scope.pendingCourseAttachVenueList.splice(i, 1);
             
               // clear textbox
                temp[0].ToBeAttached = false;
        
                $scope.AttachedCourseVenueMapList.push(temp[0]);
                i--;
                len--;

            }


            //alert('end');
        }


        $scope.attachvenueTableParams = new NgTableParams({
            count: 1000
        }, {
            dataset: $scope.AttachedCourseVenueMapList
        });

        $scope.pendingvenueTableParams = new NgTableParams({
            count: 1000
        }, {
            dataset: $scope.pendingCourseAttachVenueList
        });


    }

    $scope.moveToDetach = function () {

        var len = $scope.AttachedCourseVenueMapList.length;

        if ($scope.pendingCourseAttachVenueList === undefined || $scope.pendingCourseAttachVenueList === null)
            $scope.pendingCourseAttachVenueList = [];

        for (var i = 0; i < len; i++) {

            if ($scope.AttachedCourseVenueMapList[i].ToBeAttached) {

                var temp = $scope.AttachedCourseVenueMapList.splice(i, 1);
                temp[0].ToBeAttached = false;

                $scope.pendingCourseAttachVenueList.push(temp[0]);

                i--;
                len--;

            }
          
        }

        $scope.attachvenueTableParams = new NgTableParams({
            count: 1000
        }, {
            dataset: $scope.AttachedCourseVenueMapList
        });

        $scope.pendingvenueTableParams = new NgTableParams({
            count: 1000
        }, {
            dataset: $scope.pendingCourseAttachVenueList
        });


    }


    // for attach venue
    $scope.saveCourseAttachVenueAttached = function () {
        $rootScope.showLoading = true;
        $scope.data = {};
        $scope.data.AttachedCourseVenueMapList = $scope.AttachedCourseVenueMapList;
        $scope.data.pendingCourseAttachVenueList = $scope.pendingCourseAttachVenueList;
        $http({
            method: 'POST',
 /*           url: 'api/CourseAttachVenue/AttachedCourseVenueMap',*/
            url: 'api/CourseAttachVenue/CourseAttachVenueAttached',
            data: $scope.data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    /*   $scope.cancelMstExamCenterAdd();*/
                    //$scope.getAttachedCourseVenueMapListget();
                    //$scope.getPendingCourseAttachVenueget();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });


        
    
    };


    $scope.saveAttachVenue = function () {

        $scope.saveCourseAttachVenueAttached();

    };




    // for details of no of exam forms 
    //$scope.getExamFormDetails = function (data) {
    //    $rootScope.showLoading = true;

    //    var xml = new Object();

    //    xml.BranchId = data.BranchId;

    //    xml.ProgrammePartTermId = data.ProgrammePartTermId;
    //    /*       if (data.BranchId !== null && data.ProgrammePartTermId !== null) {*/

    //    $http({
    //        method: 'POST',
    //        url: 'api/ExamFormMaster/ExamFormDetails',
    //        data: xml,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code != "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                $scope.info = response.obj;

    //            }
    //        })
    //        .error(function (res) {

    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //    /* }*/
    //};
 /*   $scope.getMstExamCenterGet();*/


});