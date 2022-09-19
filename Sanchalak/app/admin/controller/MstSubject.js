﻿app.controller('SubjectCtrl', function ($scope, $http,$filter, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Subject";    

    /*Reset Subject*/
    $scope.resetSubject = function () {
        $scope.Subject = {};
    };

    /*Get Subject List*/
    $scope.getSubjectList = function () {
        
        var data = new Object();
               
        $http({
            method: 'POST',
            url: 'api/MstSubject/MstSubjectGet',
            
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.SubjectTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.SubjectData = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Get BOS List*/
    /*$scope.getBosList = function () {

        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudy/MstBoardOfStudyGet',
            data: $scope.Bos,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.BOSList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };*/

    /*Get Faculty List*/
    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetForDropDown',
            //data: $scope.Subject,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Add Subject*/
    $scope.addSubject = function () {

        if ($scope.Subject.SubjectName === null || $scope.Subject.SubjectName === undefined ||
            $scope.Subject.FacultyId === null || $scope.Subject.FacultyId === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstSubject/MstSubjectAdd',
                data: $scope.Subject,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.Subject = {};
                        $scope.getSubjectList();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify Subject Data*/
    $scope.modifySubjectData = function (data) {        
        $scope.showFormFlag = true;
        $scope.Subject = data;
    /*$scope.getBosList();*/
        $(window).scrollTop(0);
    };

    /*Update Subject*/
    $scope.editSubject = function () {

        if ($scope.Subject.SubjectName === null || $scope.Subject.SubjectName === undefined ||
            $scope.Subject.FacultyId === null || $scope.Subject.FacultyId === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstSubject/MstSubjectEdit',
                data: $scope.Subject,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);                        
                        $scope.getSubjectList();
                        $scope.showFormFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete Subject*/
    $scope.deleteSubject = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.Subject = data;

            $http({
                method: 'POST',
                url: 'api/MstSubject/MstSubjectDelete',
                data: $scope.Subject,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.getSubjectList();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    /*Active Enable Subject*/
    $scope.ShowSubject = function (data) {
        
        $scope.newsubject = data;

        $http({
            method: 'POST',
            url: 'api/MstSubject/MstSubjectIsActive',
            data: $scope.newsubject,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getSubjectList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Subject*/
    $scope.HideSubject = function (data) {
       
        $scope.newsubject = data;

        $http({
            method: 'POST',
            url: 'api/MstSubject/MstSubjectIsSuspended',
            data: $scope.newsubject,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getSubjectList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add New Subject*/
    $scope.newSubjectAdd = function () {
        $state.go('SubjectAdd');
    };

    /*Back to Edit Page of Subject*/
    $scope.backToList = function () {
        $state.go('SubjectEdit');
    }; 

    /*Disable Subject Data*/
    $scope.displaySubject = function (data) {
        $scope.Subject = data;
    };

    $scope.exportDataofSubject = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "SubjectData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Subject List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'SubjectName', title: 'Subject Name' },
                { columnid: 'IsActiveSts', title: 'Active Status' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.SubjectData]);
    };

});