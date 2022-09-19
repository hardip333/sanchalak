app.controller('EventClosureCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog,  NgTableParams) {

    $rootScope.pageTitle = "Manage Event Closure";

    /*Reset EventClosure*/
    $scope.resetModel = function () {
        $scope.EventClosure = {};
    };

    $scope.getExamEvent = function () {

        $http({
            method: 'POST',
            url: 'api/EventClosure/ExamEventGetForDropDown',            
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ExamMasterListGetActiveList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getInstituteByExamEvent = function () {
        
        $scope.InstituteList = {};
        data = { ExamMasterId: $scope.EventClosure.ExamMasterId };

        $http({
            method: 'POST',
            url: 'api/EventClosure/MstInstituteGetByExamEvent',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.InstituteList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getData = function () {
        debugger
        if ($scope.EventClosure.FacultyExamId == null || $scope.EventClosure.FacultyExamId === undefined
        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Faculty before Submit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.EventClosure.ExamMasterId == null || $scope.EventClosure.ExamMasterId === undefined
        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#EligibilityGroupform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Exam Event before Submit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            var data = {
                FacultyExamId: $scope.EventClosure.FacultyExamId,
                ExamMasterId: $scope.EventClosure.ExamMasterId
            };            

            $http({
                method: 'POST',
                url: 'api/EventClosure/GetDataForEventClosure',
                data: data,
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
                        $scope.TableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }

    };
      

    /*Click To Close Event*/
    $scope.CloseEvent = function (data) {
        
        $scope.newEventClosure = data;
       
        $http({
            method: 'POST',
            url: 'api/EventClosure/CloseEvent',
            data: $scope.newEventClosure,
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
                    $scope.getData();
                  
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
       
    };
       

});
