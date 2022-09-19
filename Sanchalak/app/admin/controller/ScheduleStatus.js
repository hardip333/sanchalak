app.controller('ScheduleStatusCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Schedule Status";
    $scope.selectedScheduleStatus = {};
    $rootScope.showLoading = false;
    $scope.IsInstitute = false;
    $scope.IsSection = false;
    // for show view 2
    $scope.showStatusDetailFlag = false;
    $scope.attachDetail = function (data) {

        $scope.showStatusDetailFlag = true;
        $scope.selectedScheduleStatus = data;

        $scope.getFacultyExamMapStatusGetById(data.Id);
        $scope.filter.FacultyExamMapId = data.Id;

    }
    $scope.cancelStatus = function () {
        $scope.showStatusDetailFlag = false;

        $scope.filter = {

            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammeId: 0
        };
    };

    $scope.cancelStatus();

    $scope.cancel = function (data) {

        $scope.showStatusDetailFlag = false;

        $scope.selectedScheduleStatus = data;


    };

    /*    $scope.cancel();*/
    // for show view 2 based on dropdown of schedule selection 
    $scope.attachDetailBasedonSchedule = function (data) {
        debugger;
        $scope.showStatusDetailFlag = true;
        // $scope.selectedScheduleStatus.Id = data.FacultyExamMapId;
        //alert($scope.selectedScheduleStatus.Id);

        $scope.getFacultyExamMapStatusGetById(data.FacultyExamMapId);
    }

    // for view 1 table get list
    $scope.getFacultyExamMapStatusGet = function () {

        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            /*    url: 'api/FacultyExamMap/MstFacultyExamMapStatusGet',*/
            url: 'api/ExamConfigStatus/FacultyExamMapStatusGet',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.FacultyExamMapStatusGetList = response.obj;


                    $scope.scheduleTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: $scope.FacultyExamMapStatusGetList
                    });

                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for view 1 confirm Schedule

    $scope.confirmFacultyExamMapIsConfirmed = function (data) {
        $rootScope.showLoading = true;
        $rootScope.selectedScheduleStatus = data;
        $http({
            method: 'POST',
            url: 'api/ExamConfigStatus/FacultyExamMapIsConfirmed ',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    /*      $scope.showStatusDetailFlag = false;*/
                    $scope.getFacultyExamMapStatusGet();

                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });

    };

    // for view 1 Unconfirm Schedule

    $scope.unconfirmFacultyExamMapIsUnconfirmed = function (data) {
        $rootScope.showLoading = true;
        $rootScope.selectedScheduleStatus = data;
        $http({
            method: 'POST',
            url: 'api/ExamConfigStatus/FacultyExamMapIsUnconfirmed',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getFacultyExamMapStatusGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });

    };

    // for view 1 publish Schedule

    $scope.publishFacultyExamMapIsPublished = function (data) {
        $rootScope.showLoading = true;
        $rootScope.selectedScheduleStatus = data;
        $http({
            method: 'POST',
            url: 'api/ExamConfigStatus/FacultyExamMapIsPublished',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);

                    $scope.getFacultyExamMapStatusGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });

    };

    $scope.GetFlagdata = function (data) {
        $scope.IsInstitute = false;
        $scope.IsSection = false;
        if ($cookies.get("typePrefix") == 'INS') {
            $scope.IsInstitute = true;
            $scope.IsSection = false;
        }
        else if ($cookies.get("typePrefix") == 'SEC') {
            $scope.IsSection = true;
            $scope.IsInstitute = false;
        }
        else {
            $scope.IsInstitute = false;
            $scope.IsSection = false;
        }
    }
  // for view 2 table get list 

    $scope.getFacultyExamMapStatusGetById = function (data) {

        $rootScope.showLoading = true;
        $scope.IsInstitute = false;
        $scope.IsSection = false;
        var xml = new Object();
        xml.Id = data;
    
        $http({
            method: 'POST',
/*            url: 'api/FacultyExamMap/MstFacultyExamMapGetStatusbyId',*/
            url: 'api/ExamConfigStatus/FacultyExamMapStatusGetById',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.details = response.obj;
                    
                    
                    if ($cookies.get("typePrefix") == 'INS') {
                        $scope.IsInstitute = true;
                        $scope.IsSection = false;
                    }
                    else if ($cookies.get("typePrefix") == 'SEC') {
                        $scope.IsSection = true;
                        $scope.IsInstitute = false;
                    }
                    else {
                        $scope.IsInstitute = false;
                        $scope.IsSection = false;
                    }


                }
              
            })
            .error(function (res) {
               
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
 /*   $scope.getMstExamCenterGet();*/

// for confirm courses

    $scope.confirmCourseScheduleMapIsConfirmed = function (data) {
        $rootScope.showLoading = true;


        $http({
            method: 'POST',
        /*    url: 'api/CourseScheduleMap/MstCourseScheduleMapIsConfirmed',*/
            url: 'api/ExamConfigStatus/CourseScheduleMapIsConfirmed',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getFacultyExamMapStatusGetById($scope.selectedScheduleStatus.Id);
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });


    };

    $scope.unconfirmCourseScheduleMapIsUnconfirmed = function (data) {
        $rootScope.showLoading = true;


        $http({
            method: 'POST',
       /*     url: 'api/CourseScheduleMap/MstCourseScheduleMapIsUnconfirmed',*/
            url: 'api/ExamConfigStatus/CourseScheduleMapIsUnconfirmed',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getFacultyExamMapStatusGetById($scope.selectedScheduleStatus.Id);
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

//  for  publish courses

    $scope.publishCourseScheduleMapIsPublished = function (data) {
        $rootScope.showLoading = true;
       
        $http({
            method: 'POST',
      /*      url: 'api/CourseScheduleMap/MstCourseScheduleMapIsPublished',*/
            url: 'api/ExamConfigStatus/CourseScheduleMapIsPublished',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                   
                    alert(response.obj);
                   
                }
                else {
                 
                    alert(response.obj);
                    $scope.getFacultyExamMapStatusGetById($scope.selectedScheduleStatus.Id);
                }
            })
            .error(function (res) {
                
                
                $rootScope.showLoading = false;
                alert(res.obj);
            });

    };

    //$scope.unpublishCourseScheduleMapIsUnpublished = function (data) {
    //    $rootScope.showLoading = true;

    //    $http({
    //        method: 'POST',
    //   /*     url: 'api/CourseScheduleMap/MstCourseScheduleMapIsUnpublished',*/
    //        url: 'api/CourseScheduleMap/CourseScheduleMapIsUnpublished',
    //        data: data,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.getFacultyExamMapStatusGetById($scope.selectedScheduleStatus);
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};


    // for view 2 publish hall ticket related courses

    $scope.publishHallTicketPublished = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/CourseScheduleMap/HallTicketPublished',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getFacultyExamMapStatusGetById($scope.selectedScheduleStatus.Id);
            
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });

    };
            
        
    // for view 2 Exam Type online related courses

    $scope.onlineCourseScheduleMapIsOnline = function (data) {
        $rootScope.showLoading = true;
        
        $http({
            method: 'POST',
            url: 'api/CourseScheduleMap/CourseScheduleMapIsOnline',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getFacultyExamMapStatusGetById($scope.selectedScheduleStatus.Id);

                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
                
        };


    // for view 2 Exam Type Offline related courses

    $scope.offlineCourseScheduleMapIsOffline = function (data) {
        $rootScope.showLoading = true;
  
        $http({
            method: 'POST',
            url: 'api/CourseScheduleMap/CourseScheduleMapIsOffline',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getFacultyExamMapStatusGetById($scope.selectedScheduleStatus.Id);

                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });

    };

    
/*  < !--case 1-- >*/
    //$scope.switchLanguage = function (langKey) {
    //    switch (langKey) {
    //        case 'en':
    //            $location.url('/#en');
    //            break;
    //        case 'de':
    //            $location.url('/#de');
    //            break;
    //        case 'it':
    //            $location.url('/#it');
    //            break;
    //        case 'fr':
    //            $location.url('/#fr');
    //            break;
    //        case 'es':
    //            $location.url('/#es');
    //            break;
    //        default:
    //            $location.url('/#en');
    //    }
    //    $translate.use(langKey);
    //};




      //< !--case 2-- >
    //$scope.switchLanguage = function () {
    //    langKey = $scope.selected;

    //    switch (langKey) {
    //        case 'en':
    //            alert('/#en');
    //            //$location.url('/#en');
    //            break;
    //        case 'de':
    //            alert('/#de');
    //            //$location.url('/#de');
    //            break;
    //        case 'it':
    //            alert('/#it');
    //            //$location.url('/#it');
    //            break;
    //        case 'fr':
    //            alert('/#fr');
    //            //$location.url('/#fr');
    //            break;
    //        case 'es':
    //            alert('/#es');
    //            //$location.url('/#es');
    //            break;
    //        default:
    //            alert('/#en');
    //        //$location.url('/#en');
    //    }
    //    //$translate.use(langKey);
    //}

  /*  case 3*/
    //$scope.updateSelected = function () {
    //    switch ($scope.selectedOption) {
    //        case "nextweek":
    //            alert(1);
    //            $scope.NextWeek();
    //            break;
    //        case "next15days":
    //            alert(2);
    //            $scope.next15Days();
    //            break;
    //    }
    //}




});