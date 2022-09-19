app.directive("insparttermwithcourseSelect", function () {
    return {
        templateUrl: "UI/components/insparttermwithcourseSelect.html",
        restrict: "E",
        replace: !0,
        //scope: { getList: '=' },
        controller: ["$scope", "$http", "$rootScope", function ($scope, $http, $rootScope) {

            $scope.IncProgrammePartTermGetActiveList = [];
            $scope.defaultIncPartTermList = [];
            var incparttermsObj = {
                Id : 0,
                InstancePartTermName: "Select Ins Part Term"
            }
            $scope.defaultIncPartTermList.push(incparttermsObj);

            $scope.IncProgrammePartTermGetActive = function (data) {

                $http({
                    method: 'POST',
                    url: 'api/IncProgrammePartTerm/IncProgrammePartTermGetActive',
                    data: { ProgrammeId:data },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            alert(response.obj);
                        }
                        else {

                            $scope.tempList = response.obj;
                            $scope.IncProgrammePartTermGetActiveList = $scope.defaultIncPartTermList.concat($scope.tempList);
                        }
                    })
                    .error(function (res) {
                        alert(res.obj);
                    });
            }
   /*         $scope.IncProgrammePartTermGetActive();*/


        }],
        link: function (e, t, a) {
        }
    }
})