app.directive("programmeindependentSelect", function () {
    return {
        templateUrl: "UI/components/programmeindependentSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.activeMstProgrammeListGetForDropDown = [];
            $scope.defaultMstProgrammeListGetList = [];
            var progObj = {
                Id: 0,
                ProgrammeName: "Select Programme"
            }
            $scope.defaultMstProgrammeListGetList.push(progObj);

            $scope.getActiveMstProgrammeListGetForDropDown = function () {

                $http({
                    method: 'POST',
                    url: 'api/MstProgramme/MstProgrammeListGetForDropDown',
                    data: {},
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.activeMstProgrammeListGetForDropDown = $scope.defaultMstProgrammeListGetList.concat($scope.tempList);

                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
            $scope.getActiveMstProgrammeListGetForDropDown();


        }],
        link: function (e, t, a) {
        }
    }
})