app.controller('SeatNumberWiseBlockChangeCtrl', function ($scope, $http, $rootScope, $state, $cookies, Upload, $mdDialog, $localStorage, NgTableParams, httpRequestInterceptor) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    }
    $rootScope.pageTitle = "Seat Number Wise Block Change";
    $scope.SeatNumberWiseBlockChange = {};
    
    
    
    

    $scope.RadioTransFlag = false;
    $scope.RadioDualFlag = false;
    $scope.DefaultRadioFlag = false;

    $scope.IsInstitute = false;
    $scope.IsSection = false;

    var today = new Date();
    var date = today.getDate();
    var month = today.getMonth() + 1;
    var year = today.getFullYear();

    $scope.expand_row = function (id) {
        let element = document.getElementById('expand' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + id).innerHTML = "+"
            element.add("collapse")
        }
    }

    
    //Function for Find data using PRN
    $scope.getSeatNumberWiseBlockChange = function (SeatNumber) {
        $scope.ShowApplicantFlag = false;
        $scope.SubmitDualflag = false;
        $scope.MsgFlag = false;
        $scope.HideFlag = true;
        //debugger
        if ($scope.SeatNumberWiseBlockChange.SeatNumber == null || $scope.SeatNumberWiseBlockChange.SeatNumber === undefined) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Enter PRN")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
            $scope.ShowApplicantFlag = false;
            $scope.SubmitDualflag = false;
            $scope.MsgFlag = false;
            $scope.HideFlag = true;
        }
        else {
            $http({
                method: 'POST',
                url: 'api/SeatNumberWiseBlockChange/SeatNumberWiseBlockChange',
                //data: { SeatNumber: $scope.SeatNumberWiseBlockChange.SeatNumber },
                data: $scope.SeatNumberWiseBlockChange,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    if (response.response_code == "201") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $mdDialog.show(
                            $mdDialog.alert()
                                .parent(angular.element(document.querySelector('#popupContainer')))
                                .clickOutsideToClose(true)
                                .title("Alert")
                                .textContent(response.obj)
                                .ariaLabel('Alert Dialog Demo')
                                .ok('Okay')
                        );
                        $scope.SeatNumberWiseBlockChange = {};
                        $scope.MsgFlag = false;
                        $scope.HideFlag = true;
                    }
                    else {
                        $scope.SeatNumberSearchData = response.obj;
                        $scope.SeatNum = response.obj[0];
                        $scope.ShowApplicantFlag = true;
                        $scope.MsgFlag = true;
                        //if (response.obj[0].Message1 == 'Admitted in one Programme.') {
                        //    $scope.SubmitDualflag = true;
                        //    $scope.MsgFlag = false;
                        //    $scope.HideFlag = true;
                        //    //$scope.getProgrammeListforDualAdm();
                        //}
                        
                    }

                })
                .error(function (res) {
                    $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }

    
    $scope.getExamBlock = function (ExamBlockId,ExamVenueId,MstPaperId) {
         //debugger;
        $scope.FinalModel = {};
        $scope.FinalModel.ExamBlockId = ExamBlockId;
        $scope.FinalModel.ExamVenueId = ExamVenueId;
        $scope.FinalModel.MstPaperId = MstPaperId;
        $http({
            method: 'POST',
            url: 'api/SeatNumberWiseBlockChange/ExamBlockGet',
            data: { ExamBlockId: $scope.FinalModel.ExamBlockId, ExamVenueId: $scope.FinalModel.ExamVenueId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.ExamBlockList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code       
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.updateSeatNumberWiseBlockChange = function (finalmodel, index,MstPaperId) {
       // debugger
        
        //$scope.ExamBlockId = data.ExamBlockId; 
        //alert(finalmodel.ExamBlock[index].ExamBlockId);
        $http({
            method: 'POST',
            url: 'api/SeatNumberWiseBlockChange/SeatNumberWiseBlockChangeEdit',
            data: { MstPaperId: MstPaperId, ExamBlockId: finalmodel.ExamBlock[index].ExamBlockId },
            //data: $scope.FinalModel,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.SeatNumberWiseBlockChange[index].MstPaperId = "1";

                }
                else {
                   // debugger
                    alert(response.obj);
                    $scope.getSeatNumberWiseBlockChange();
                    

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });




    }
   

});