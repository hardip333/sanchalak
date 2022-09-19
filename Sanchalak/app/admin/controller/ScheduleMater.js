app.controller('ScheduleMasterCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Schedule Master";


    $rootScope.showLoading = false;
    $scope.showScheduleMaterListFlag = false;
    $scope.showAttachedCoursesFlag = false;
    $scope.showNewCoursesFlag = false;
    // for 2nd table schedule master 
     // for edit schedule master 
    $scope.editScheduleMaster = function (data) {

        $scope.ScheduleMaster = data;
        $scope.showScheduleMaterListFlag = true;
       
        $scope.getExamFeeHeadListForFacultyExamMapGet();
        $scope.getMstInstituteGetByFacIdAndInsType($scope.ScheduleMaster.FacultyId);
    };

    $scope.cancelMstFacultyExamMap = function () {
        $scope.ScheduleMaster = {
            FacultyId: 0,
            ExamMasterId: 0,
            InstituteId: 0
        };

        $scope.filter = {
            ExamMasterId: 0,
            AcademicYearId: 0,
            ProgrammeId: 0,
            ProgrammeInstanceId: 0,
            FacultyId:0 

        };
    };

    $scope.cancelMstFacultyExamMap();

    // for  table 2 get data for old api
    //$scope.getMstFacultyExamMap = function () {
    //    $rootScope.showLoading = true;

    //    var xml = new Object();
    //    xml.ExamMasterId = $scope.filter.ExamMasterId;
    //    $scope.MstFacultyExamMap = [];

    //    $http({
    //        method: 'POST',
    //        url: 'api/FacultyExamMap/MstFacultyExamMapGet',
    //        data: xml,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code != "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                $scope.MstFacultyExamMap = response.obj;

    //                $scope.ScheduleMasterTableParams = new NgTableParams({
    //                    count: 1000
    //                }, {
    //                    dataset: $scope.MstFacultyExamMap
    //                });
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};
    //$scope.getMstFacultyExamMap();


     // for  table 2 get data for new api
    $scope.getFacultyExamMapListGet = function () {
        $rootScope.showLoading = true;
        
        var xml = new Object();
        xml.ExamMasterId = $scope.filter.ExamMasterId;
        $scope.FacultyExamMapListGetList = [];

        $http({
            method: 'POST',
            url: 'api/FacultyExamMap/FacultyExamMapListGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.FacultyExamMapListGetList = response.obj;

                    $scope.ScheduleMasterTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: $scope.FacultyExamMapListGetList
                    });
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    //$scope.getFacultyExamMapListGet();


    // old api of save schdeule master
    //$scope.saveMstFacultyExamMapAdd = function () {
    //    $rootScope.showLoading = true;

    //    $http({
    //        method: 'POST',
    //        url: 'api/FacultyExamMap/MstFacultyExamMapAdd',
    //        data: $scope.ScheduleMaster,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {

    //                alert(response.obj);
    //                /*           $scope.cancelMstFacultyExamMap();*/
    //                $scope.getMstFacultyExamMap();
    //            }
    //        })
    //        .error(function (res) {

    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

    // for save
    $scope.saveFacultyExamMapAdd = function (data) {
        $rootScope.showLoading = true;
        $scope.ScheduleMaster = data;
        $http({
            method: 'POST',
            url: 'api/FacultyExamMap/FacultyExamMapAdd',
            data: $scope.ScheduleMaster,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
           
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
/*                    alert(response.obj);*/
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
               /*     $scope.cancelExamEvent();*/
                    $scope.getFacultyExamMapListGet();
                    $scope.showScheduleMaterListFlag = false;

                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for edit with api 
    $scope.editFacultyExamMapEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.ScheduleMaster = data;
      
        $http({
            method: 'POST',
            url: 'api/FacultyExamMap/FacultyExamMapEdit',
            data: $scope.ScheduleMaster,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
        
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
         
                    $scope.getFacultyExamMapListGet();
                    $scope.showScheduleMaterListFlag = false;
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save schedule master with condition
    $scope.saveScheduleMaster = function (data) {
  
        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveFacultyExamMapAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editFacultyExamMapEdit(data);

        }

    }


    $scope.hideScheduleMaster = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/FacultyExamMap/FacultyExamMapIsActive',
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
                    $scope.getFacultyExamMapListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.showScheduleMaster = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/FacultyExamMap/FacultyExamMapIsInactive',
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
                    $scope.getFacultyExamMapListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for fee head list in table 1
    $scope.getExamFeeHeadListForFacultyExamMapGet = function () {
        $rootScope.showLoading = true;

        var xml = new Object();
        xml.StartDateOfExam = $scope.ScheduleMaster.StartDateOfExam;
        xml.LastDateOfFeesPaymentForStudent = $scope.ScheduleMaster.LastDateOfFeesPaymentForStudent;
        xml.LastDateOfFeesPaymentForCollege = $scope.ScheduleMaster.LastDateOfFeesPaymentForCollege;
  /*      $scope.MstExamFeeHeadListForFacultyExamMapList = [];*/

        $http({
            method: 'POST',
      /*      url: 'api/ExamFeeHead/MstExamFeeHeadListForFacultyExamMap',*/
            url: 'api/ExamFeeHead/getExamFeeHeadListForFacultyExamMap',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.ExamFeeHeadListForFacultyExamMapList = response.obj;

                    var len = $scope.ExamFeeHeadListForFacultyExamMapList.length;

                    var LastDateOfFeesPaymentForStudent = new Date($scope.ScheduleMaster.LastDateOfFeesPaymentForStudent);
                    var LastDateOfFeesPaymentForCollege = new Date($scope.ScheduleMaster.LastDateOfFeesPaymentForCollege);

                    for (var i = 0; i < len; i++) {

                        if (i != 0) {
                            $scope.ExamFeeHeadListForFacultyExamMapList[i].DayRangeTxt = $scope.ExamFeeHeadListForFacultyExamMapList[i - 1].DayRange + " - " + $scope.ExamFeeHeadListForFacultyExamMapList[i].DayRange;
                        }
                        else {
                            $scope.ExamFeeHeadListForFacultyExamMapList[i].DayRangeTxt = "0 - " + $scope.ExamFeeHeadListForFacultyExamMapList[i].DayRange;

                        }

                        var tentativeDateForStudent = new Date($scope.ScheduleMaster.LastDateOfFeesPaymentForStudent);
                        var tentativeDateForCollege = new Date($scope.ScheduleMaster.LastDateOfFeesPaymentForCollege);

                        tentativeDateForStudent.setDate(LastDateOfFeesPaymentForStudent.getDate() + parseInt($scope.ExamFeeHeadListForFacultyExamMapList[i].DayRange));
                        tentativeDateForCollege.setDate(LastDateOfFeesPaymentForCollege.getDate() + parseInt($scope.ExamFeeHeadListForFacultyExamMapList[i].DayRange));

                        $scope.ExamFeeHeadListForFacultyExamMapList[i].tentativeDateForStudent = tentativeDateForStudent.toDateString();
                        $scope.ExamFeeHeadListForFacultyExamMapList[i].tentativeDateForCollege = tentativeDateForCollege.toDateString();

                    }

                    $scope.feeheadTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: $scope.ExamFeeHeadListForFacultyExamMapList
                    });
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    //$scope.getExamFeeHeadListForFacultyExamMapGet();



    //$scope.cancelattachcourses = function () {
    //    $scope.venue = {};
    //};

    //$scope.cancelattachcourses();

    // for 1st table
    $scope.cancelScheduleMaster = function () {
        $scope.showScheduleMaterListFlag = false;
        //$scope.showAttachedCoursesFlag = false;
        //$scope.showNewCoursesFlag = false;

        $scope.ScheduleMaster = {
   
            ExamMasterId: 0,
            FacultyId: 0

        };
    };
    $scope.cancelScheduleMaster();

    $scope.backToForm = function () {

        $scope.ScheduleMaster = {
            ExamMasterId: 0,
            FacultyId: 0

        };
        $scope.showScheduleMaterListFlag = true;
        $scope.showAttachedCoursesFlag = false;
        $scope.showNewCoursesFlag = false;
    }

    // for attachcorses // for table 3
    $scope.attachCourses = function (data) {

        $scope.showAttachedCoursesFlag = true;
        $scope.showScheduleMaterListFlag = false;
        $scope.showNewCoursesFlag = false;
        $scope.selectedSchedule = data;
        $scope.getCourseScheduleMapListGetActive(data);
        $scope.selectedSchedule = data;

    }

    $scope.cancelAttachCourses = function () {
        $scope.showAttachedCoursesFlag = false;
        //$scope.showScheduleMaterListFlag = false;
        //$scope.showNewCoursesFlag = false;

        //$scope.selectedSchedule = {};
    };

    //$scope.cancelAttachCourses();

    // for addnewattache courses table 4
    $scope.addNewCourse = function () {
        $scope.new = {};
        $scope.showNewCoursesFlag = true;
        
        $scope.showAttachedCoursesFlag = false;
        $scope.showScheduleMaterListFlag = false;
        $scope.filter = {
            ExamMasterId: 0,
            AcademicYearId: 0,
            ProgrammeId: 0,
            ProgrammeInstanceId: 0,
            FacultyId: 0

        };
        $scope.PendingCourseScheduleMapList = {};

      
    }
    // table 4


    $scope.cancelNewCouses = function () {

        $scope.showNewCoursesFlag = false ;

        $scope.showAttachedCoursesFlag = true;
    
        $scope.showScheduleMaterListFlag = false;




    };
  /*  $scope.cancelNewCouses();*/

    // for get only active courses table 3
    $scope.getCourseScheduleMapListGetActive = function (data) {
      
        $rootScope.showLoading = true;

        var xml = new Object();
        xml.FacultyExamMapId = data.Id;
        $scope.selectedSchedule = data;
        xml.FacultyId = $scope.selectedSchedule.FacultyId;
     
        
    
        $http({
            method: 'POST',
        /*    url: 'api/CourseScheduleMap/MstCourseScheduleMapGetActive',*/
            url: 'api/CourseScheduleMap/CourseScheduleMapListGetActive',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
       
                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.CourseScheduleMapListGetActiveList = response.obj;

                    $scope.newcoursesTableParams = new NgTableParams({
                    }, {
                        dataset: $scope.CourseScheduleMapListGetActiveList
                    });
                }

            })
            .error(function (res) {
                
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save schedule table 4

    $scope.getPendingCourseScheduleMapList = function () {

        $rootScope.showLoading = true;

        var xml = new Object();
        xml.ProgrammeId = $scope.filter.ProgrammeId;
        xml.FacultyExamMapId = $scope.selectedSchedule.Id;
        xml.ExamMasterId = $scope.selectedSchedule.ExamMasterId;

        $http({
            method: 'POST',
            url: 'api/CourseScheduleMap/PendingCourseScheduleMapList',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.PendingCourseScheduleMapList = response.obj;

                    $scope.newTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: $scope.PendingCourseScheduleMapList
                    });
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for update button in table 4 
    $scope.saveCourseScheduleMapListAdd = function (data) {
        //debugger

        //alert($scope.filter.ExamFeeStartDate);
        //alert($scope.filter.ExamFeeEndDateForStudent);
        //alert($scope.filter.ExamFeeEndDateforFaculty);

        if ($scope.filter.ExamFeeStartDate > $scope.filter.ExamFeeEndDateForStudent) {
            alert("Please Select Proper ExamFeeEndDateForStudent");
        }
        else if ($scope.filter.ExamFeeEndDateForStudent > $scope.filter.ExamFeeEndDateforFaculty) {
            alert("Please Select Proper ExamFeeEndDateforFaculty");
        }
        else if (($scope.filter.ExamFeeStartDate && $scope.filter.ExamFeeEndDateForStudent) > $scope.filter.ExamFeeEndDateforFaculty) {
            alert("Please Select Proper ExamFeeStartDate and ExamFeeEndDateForStudent");
        }
        else {
            $rootScope.showLoading = true;
            ///*    $scope.PendingCourseScheduleMapList = data;*/
            for (var i in $scope.PendingCourseScheduleMapList) {
                $scope.PendingCourseScheduleMapList[i].ExamFeeStartDate = $scope.filter.ExamFeeStartDate;
                $scope.PendingCourseScheduleMapList[i].ExamFeeEndDateForStudent = $scope.filter.ExamFeeEndDateForStudent;
                $scope.PendingCourseScheduleMapList[i].ExamFeeEndDateforFaculty = $scope.filter.ExamFeeEndDateforFaculty;
            }

            $http({
                method: 'POST',
                /*        url: 'api/CourseScheduleMap/MstCouseScheduleMapListAdd',*/
                url: 'api/CourseScheduleMap/CourseScheduleMapListAdd',
                data: $scope.PendingCourseScheduleMapList,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {

                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        if (response.obj != null) {

                            $scope.showNewCoursesFlag = false;
                            $scope.showAttachedCoursesFlag = true;
                            $scope.getCourseScheduleMapListGetActive($scope.selectedSchedule);
                            $scope.getPendingCourseScheduleMapList();
                        }

                    }
                })
                .error(function (res) {
                    $rootScope.showLoading = false;
                    alert(res.obj);
                });
        }
       
    };

    // for edit schedule table 4

    $scope.fresherFlag = false;
    $scope.editCourses = function (data) {

        data.fresherFlag = true;
        $scope.newcourses = data;
        //$scope.getCourseScheduleMapListGetActive();
        var appExamStartDate = $scope.newcourses.ExamFeeStartDateView.split("-");
        $scope.newcourses.ExamFeeStartDate = new Date(appExamStartDate[2], (appExamStartDate[1] >= 1) ? (appExamStartDate[1] - 1) : appExamStartDate[1], appExamStartDate[0]);

        var appExamFeeDate = $scope.newcourses.ExamFeeEndDateForStudentView.split("-");
        $scope.newcourses.ExamFeeEndDateForStudent = new Date(appExamFeeDate[2], (appExamFeeDate[1] >= 1) ? (appExamFeeDate[1] - 1) : appExamFeeDate[1], appExamFeeDate[0]);

        var appExamFactDate = $scope.newcourses.ExamFeeEndDateforFacultyView.split("-");
        $scope.newcourses.ExamFeeEndDateforFaculty = new Date(appExamFactDate[2], (appExamFactDate[1] >= 1) ? (appExamFactDate[1] - 1) : appExamFactDate[1], appExamFactDate[0]);
        
    }

    // for save schedule with condition
    $scope.saveCourses = function (data) {

        $scope.fresherFlag = true;

        if (data.Id !== 0) {
            $scope.editCourseScheduleMapEdit(data);

        } else if (data.Id === 0) {
            $scope.saveCourseScheduleMapAdd(data);
        }
        
    }

    // for edit schedule
    $scope.editCourseScheduleMapEdit = function (data) {

        //debugger
        $rootScope.showLoading = true;
        $scope.fresherFlag = true;
        $scope.newcourses = data;

        if ($scope.newcourses.ExamFeeStartDate > $scope.newcourses.ExamFeeEndDateForStudent) {
            alert("Please Select Proper ExamFeeEndDateForStudent");
        }
        else if ($scope.newcourses.ExamFeeEndDateForStudent > $scope.newcourses.ExamFeeEndDateforFaculty) {
            alert("Please Select Proper ExamFeeEndDateforFaculty");
        }
        else if (($scope.newcourses.ExamFeeStartDate && $scope.newcourses.ExamFeeEndDateForStudent) > $scope.newcourses.ExamFeeEndDateforFaculty) {
            alert("Please Select Proper ExamFeeStartDate and ExamFeeEndDateForStudent");
        }
        else {
            
                
            $http({
                method: 'POST',
                url: 'api/CourseScheduleMap/CourseScheduleMapEdit',
                data: $scope.newcourses,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code !== "200") {

                        alert(response.obj);
                    }
                    else {

                        alert(response.obj);
                        $scope.fresherFlag = true;
                        /*     $scope.cancelMstTimeTableMasterAdd();*/
                        $scope.getCourseScheduleMapListGetActive($scope.selectedSchedule);
                    }
                })
                .error(function (res) {

                    $rootScope.showLoading = false;
                    alert(res.obj);
                });


             }
        
    };

    // for save schedule
    $scope.saveCourseScheduleMapAdd = function (data) {

        $rootScope.showLoading = true;
        $scope.fresherFlag = true;
        $scope.newcourses = data;

        $http({
            method: 'POST',
            url: 'api/CourseScheduleMap/CourseScheduleMapAdd',
            data: $scope.newcourses,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.fresherFlag = false;
                    /*     $scope.cancelMstTimeTableMasterAdd();*/
                    $scope.getCourseScheduleMapListGetActive($scope.selectedSchedule);
                }
            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };


//    $scope.saveCourseScheduleMapAdd = function (data) {
//        $rootScope.showLoading = true;
//        $scope.fresherFlag = true;


//        $http({
//            method: 'POST',
///*            url: 'api/CourseScheduleMap/MstCourseScheduleMapAdd',*/
//            url: 'api/CourseScheduleMap/CourseScheduleMapAdd',
//            data: data ,
//            headers: { "Content-Type": 'application/json' }
//        })
//            .success(function (response) {
//                $rootScope.showLoading = false;

//                if (response.response_code !== "200") {
//                    alert(response.obj);
//                }
//                else {
//                     alert(response.obj);
//                    $scope.fresherFlag = false;
//                    $scope.getCourseScheduleMapListGetActive($scope.selectedSchedule);
//                }
//            })
//            .error(function (res) {
//                $rootScope.showLoading = false;
//                alert(res.obj);
//            });
//    };

    // for delete schedule table 4

    $scope.MstCourseScheduleMapDelete = function (data) {
        debugger;
        $http({
            method: 'POST',
            url: 'api/CourseScheduleMap/CourseScheduleMapDelete',
            data: data ,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    debugger;
                    $scope.getCourseScheduleMapListGetActive($scope.selectedSchedule);
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };




});



















































