var pipes = [];

apiCall = function (req) {
  return "Hello" + req;
};
wrap = function (req, next) {
  console.log("start wrap");
  var response = next(req);
  console.log("end wrap");
  return response;
};
tryWrap = function (req, next) {
    console.log("start tryWrap");
    var response = next(req);
    console.log("end tryWrap");
    return response;
  };
addPipe = function (pipe) {
  if (pipes.length === 0) {
    pipes.push((req) => pipe(req, apiCall));
  } else {
    let prevPipe = pipes[pipes.length - 1];
    pipes.push((req) => pipe(req, prevPipe));
  }
};
build = function(){
    return pipes[pipes.length-1]
}
addPipe(wrap);
addPipe(tryWrap);
var mainPipe = build();

mainPipe("Hello..")
