use axum::{
    routing::get,
    Router,
    response::{Json, Response},
    http::StatusCode,
};
use std::sync::Arc;
use axum::body::Body;
use serde_json::{json};
use serde::{Serialize};


#[derive(Serialize)]
struct Bars {
    data: Vec<Bar>,
}


#[derive(Clone, Copy, Serialize)]
struct Bar {
    open: f32,
    high: f32,
    low: f32,
    close: f32,
}

struct AppSate {
    bars: Bars,
    static_bars: String,
}

#[tokio::main]
async fn main() {
    let bars = Bars {
        data: vec![Bar {
            open: 432.3,
            high: 234.3,
            low: 324.3,
            close: 23.,
        }; 100],
    };

    let static_bars = serde_json::to_string(&Bars {
        data: vec![Bar {
            open: 432.3,
            high: 234.3,
            low: 324.3,
            close: 23.,
        }; 100],
    }).unwrap();

    let shared_state = Arc::new(AppSate { bars: bars, static_bars: static_bars });

    // build our application with a single route
    let app = Router::new()
        .route("/", get(|| async { "Hello, World!" }))
        .route("/bars", get({
            let s = Arc::clone(&shared_state);
            //let bars = (*ss).clone();
            Json(json!(s.bars))
        }))
        .route("/bars-static", get(move || async move {
            let s = Arc::clone(&shared_state);
            Response::builder()
                .status(StatusCode::OK)
                .body(Body::from(s.static_bars.clone()))
                .unwrap()
        }));

    // run our app with hyper, listening globally on port 3000
    let listener = tokio::net::TcpListener::bind("0.0.0.0:3000").await.unwrap();
    axum::serve(listener, app).await.unwrap();
}